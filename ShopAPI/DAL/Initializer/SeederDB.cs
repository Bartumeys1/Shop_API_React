using DAL.Data.Constants;
using DAL.Entities.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DAL.Initializer
{
    public static class SeederDB
    {
        public static void SeedData (this IApplicationBuilder app )
        {
            using (IServiceScope scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                UserManager<UserEntity> userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
                RoleManager<RoleEntity> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<RoleEntity>>();
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
                
            }
        }
    }
}
