using DAL.Data.Constants;
using DAL.Entities;
using DAL.Entities.Identity;
using DAL.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
                var productsRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();

                var context = scope.ServiceProvider.GetRequiredService<AppEFContext>();

                context.Database.Migrate();

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

                if (!categoryRepository.Categories.Any())
                {
                    CategoryEntity[] categories = {
                        new CategoryEntity() { Name = "Ноутбуки", DateCreated = DateTime.Now.ToUniversalTime() },
                        new CategoryEntity() { Name = "Одяг", DateCreated = DateTime.Now.ToUniversalTime()}

                    };

                    foreach (var item in categories)
                    {
                        await categoryRepository.Create(item);
                    }
                };


                if(!productsRepository.Products.Any())
                { 
                    List<ProductEntity> products = new List<ProductEntity>() { 
                        // Ноутбуки
                        new ProductEntity() {
                            Name="Hp Pavelion", 
                            CategoryId = 1, 
                            Description="Простий ноутбук Hp Pavelion ... ", 
                            Price= 200.12F, 
                            DateCreated = DateTime.Now.ToUniversalTime()
                        },
                        new ProductEntity() {
                            Name="Alienwaer",
                            CategoryId = 1,
                            Description=" Найкращий ноутбук Alienwaer ... ",
                            Price= 1350.0F,
                            DateCreated = DateTime.Now.ToUniversalTime()
                        },
                        new ProductEntity() {
                            Name="Macbook",
                            CategoryId = 1,
                            Description="Ноутбук для дівчот ... ",
                            Price= 2000.0F,
                            DateCreated = DateTime.Now.ToUniversalTime()
                        },

                        //Одежа
                        new ProductEntity() {
                            Name="Jeans",
                            CategoryId = 2,
                            Description=" Пара гарних недорогіх джинсів ... ",
                            Price= 19.50F,
                            DateCreated = DateTime.Now.ToUniversalTime()
                        }, 
                        new ProductEntity() {
                            Name="Sweater",
                            CategoryId = 2,
                            Description="Недорогий світер ... ",
                            Price= 25.0F,
                            DateCreated = DateTime.Now.ToUniversalTime()
                        },
                    };

                    foreach (var item in products)
                    {
                        await productsRepository.Create(item);
                    }
                }
            }
        }
    }
}
