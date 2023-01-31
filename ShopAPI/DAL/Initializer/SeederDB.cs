using DAL.Data.Constants;
using DAL.Entities;
using DAL.Entities.Identity;
using DAL.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DAL.Initializer
{
    public static class SeederDB
    {
        public static async void SeedData (this IApplicationBuilder app )
        {
            using (IServiceScope scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                UserManager<UserEntity> userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
                RoleManager<RoleEntity> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<RoleEntity>>();
                var categoryRepository = scope.ServiceProvider.GetRequiredService<ICategoryRepository>();
                if(!roleManager.Roles.Any())
                {
                    IdentityResult result = roleManager.CreateAsync(new RoleEntity
                    {
                        Name= Roles.Administrator
                    }).Result;

                    result = roleManager.CreateAsync(new RoleEntity
                    {
                        Name = Roles.User
                    }).Result;
                }

                if(!userManager.Users.Any()) 
                {
                    string admin = "admin@gmail.com";
                    UserEntity user = new UserEntity
                    {
                        Email = admin,
                        UserName = admin,
                        FirstName = "Andriy",
                        LastName = "Steapniuk"
                    };

                    IdentityResult result = userManager.CreateAsync(user,"123456").Result;
                    result = userManager.AddToRoleAsync(user, Roles.Administrator).Result;
                }
                
                if(!categoryRepository.Categories.Any())
                {
                    CategoryEntity[] categories = { 
                        new CategoryEntity() { Id = 1, Name = "Ноутбуки", DateCreated = DateTime.Now.ToUniversalTime() },
                        new CategoryEntity() { Id = 2, Name = "Одяг", DateCreated = DateTime.Now.ToUniversalTime()}
                    };

                    foreach (var item in categories)
                    {
                        await categoryRepository.Create(item);
                    }
                }
            }
        }
    }
}
