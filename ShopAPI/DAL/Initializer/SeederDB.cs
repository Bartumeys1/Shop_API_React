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


                    List<ProductEntity> products = new List<ProductEntity>() { 
                        // Ноутбуки
                        new ProductEntity() { Id = 1, 
                            Name="Hp Pavelion", 
                            Category = categories[0], 
                            Description="Простий ноутбук Hp Pavelion ... ", 
                            Price= 200.12F, 
                            DateCreated = DateTime.Now.ToUniversalTime()
                        },
                        new ProductEntity() { Id = 2,
                            Name="Alienwaer",
                            Category = categories[0],
                            Description=" Найкращий ноутбук Alienwaer ... ",
                            Price= 1350.0F,
                            DateCreated = DateTime.Now.ToUniversalTime()
                        },
                        new ProductEntity() { Id = 3,
                            Name="Macbook",
                            Category = categories[0],
                            Description="Ноутбук для дівчот ... ",
                            Price= 2000.0F,
                            DateCreated = DateTime.Now.ToUniversalTime()
                        },

                        //Одежа
                        new ProductEntity() { Id = 4,
                            Name="Jeans",
                            Category = categories[1],
                            Description=" Пара гарних недорогіх джинсів ... ",
                            Price= 19.50F,
                            DateCreated = DateTime.Now.ToUniversalTime()
                        }, 
                        new ProductEntity() { Id = 5,
                            Name="Sweater",
                            Category = categories[1],
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
