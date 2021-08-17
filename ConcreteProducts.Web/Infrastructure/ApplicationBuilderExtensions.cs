﻿namespace ConcreteProducts.Web.Infrastructure
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using ConcreteProducts.Data;
    using ConcreteProducts.Data.Models;
    using ConcreteProducts.Data.Models.Enumerations;

    using static GlobalConstants;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder ApplyMigrations(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            var dbContext = services.GetRequiredService<ConcreteProductsDbContext>();
            dbContext.Database.Migrate();

            SeedColors(dbContext);
            SeedCategories(dbContext);
            SeedWarehouses(dbContext);
            SeedProducts(dbContext);
            SeedProductColor(dbContext);
            SeedRoles(services);
            SeedAdministrator(services);

            return app;
        }

        private static void SeedColors(ConcreteProductsDbContext data)
        {
            if (data.Colors.Any())
            {
                return;
            }

            data.Colors.AddRange(new[]
            {
                new Color {Name = "Grey"},
                new Color {Name = "Washed Grey"},
                new Color {Name = "Red"},
                new Color {Name = "Yellow"},
                new Color {Name = "Washed White"},
            });

            data.SaveChanges();
        }

        private static void SeedCategories(ConcreteProductsDbContext data)
        {
            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
                new Category {Name = "Pavement"},
                new Category {Name = "Sidewalk"},
                new Category {Name = "Flooring"},
                new Category {Name = "Border"},
                new Category {Name = "Brick"},
                new Category {Name = "Cover"},
            });

            data.SaveChanges();
        }

        private static void SeedWarehouses(ConcreteProductsDbContext data)
        {
            if (data.Warehouses.Any())
            {
                return;
            }

            data.Warehouses.AddRange(new[]
            {
                new Warehouse {Name = "Basic"}
            });

            data.SaveChanges();
        }

        private static void SeedProducts(ConcreteProductsDbContext data)
        {
            if (data.Products.Any())
            {
                return;
            }

            data.Products.AddRange(new[]
            {
                new Product {
                    Name = "Pave",
                    Dimensions = "10/10/6",
                    UnitOfMeasurement = UnitOfMeasurement.SquareMeters,
                    QuantityInPalletInUnitOfMeasurement = 9.36,
                    QuantityInPalletInPieces = 936,
                    CountInUnitOfMeasurement = 100,
                    Weight = 1.34,
                    ImageUrl = "https://www.presbetonel.bg/wp-content/uploads/2016/08/Pave-Klasik-1-1600x1067.jpg",
                    CategoryId = data.Categories.Where(c=>c.Name == "Pavement").Select(c=>c.Id).FirstOrDefault(),
                    },
                new Product {
                    Name = "Pave",
                    Dimensions = "20/20/6",
                    UnitOfMeasurement = UnitOfMeasurement.SquareMeters,
                    QuantityInPalletInUnitOfMeasurement = 10.4,
                    QuantityInPalletInPieces = 260,
                    CountInUnitOfMeasurement = 25,
                    Weight = 5.08,
                    ImageUrl = "https://www.presbetonel.bg/wp-content/uploads/2016/08/Grand-Klasik-1-1600x1067.jpg",
                    CategoryId = data.Categories.Where(c=>c.Name == "Pavement").Select(c=>c.Id).FirstOrDefault(),
                    },
                new Product {
                    Name = "Pave",
                    Dimensions = "20/10/6",
                    UnitOfMeasurement = UnitOfMeasurement.SquareMeters,
                    QuantityInPalletInUnitOfMeasurement = 10.4,
                    QuantityInPalletInPieces = 520,
                    CountInUnitOfMeasurement = 50,
                    Weight = 2.54,
                    ImageUrl = "http://vulkan.bg/115-thickbox_default/trotoarno-pave-20106.jpg",
                    CategoryId = data.Categories.Where(c=>c.Name == "Pavement").Select(c=>c.Id).FirstOrDefault(),
                    },
                new Product {
                    Name = "Tile",
                    Dimensions = "30/30/6",
                    UnitOfMeasurement = UnitOfMeasurement.SquareMeters,
                    QuantityInPalletInUnitOfMeasurement = 7.02,
                    QuantityInPalletInPieces = 78,
                    CountInUnitOfMeasurement = 11.11,
                    Weight = 13.27,
                    ImageUrl = "https://sd.cd9.xyz/image/1990-plochka-trotoarna-optik-30304-zhalt-3-small-1665.jpg",
                    CategoryId = data.Categories.Where(c=>c.Name == "Sidewalk").Select(c=>c.Id).FirstOrDefault(),
                    },


                new Product {
                    Name = "Бордюр",
                    Dimensions = "50/15/25",
                    UnitOfMeasurement = UnitOfMeasurement.Meters,
                    QuantityInPalletInUnitOfMeasurement = 18,
                    QuantityInPalletInPieces = 36,
                    CountInUnitOfMeasurement = 2,
                    Weight = 36.11,
                    ImageUrl = "https://www.masterhaus.bg/thumbs/246x246/products/11/03/11/1103110014-15-tuhla-beton_246x246_pad_478b24840a.jpg",
                    CategoryId = data.Categories.Where(c=>c.Name == "Border").Select(c=>c.Id).FirstOrDefault(),
                    },
                new Product {
                    Name = "Тухла",
                    Dimensions = "40/20/20",
                    UnitOfMeasurement = UnitOfMeasurement.SquareMeters,
                    QuantityInPalletInUnitOfMeasurement = 3.84,
                    QuantityInPalletInPieces = 48,
                    CountInUnitOfMeasurement = 12.5,
                    Weight = 25,
                    ImageUrl = "https://www.masterhaus.bg/thumbs/246x246/products/11/03/11/1103110014-15-tuhla-beton_246x246_pad_478b24840a.jpg",
                    CategoryId = data.Categories.Where(c=>c.Name == "Brick").Select(c=>c.Id).FirstOrDefault(),
                    },

            });

            data.SaveChanges();
        }

        private static void SeedProductColor(ConcreteProductsDbContext data)
        {
            if (data.ProductColors.Any())
            {
                return;
            }

            data.ProductColors.AddRange(new[]
            {
                new ProductColor {
                    ColorId = data.Colors.Where(c=>c.Name == "Red").Select(c=>c.Id).FirstOrDefault(),
                    ProductId = data.Products.Where(p=>p.Dimensions == "10/10/6").Select(p=>p.Id).FirstOrDefault()
                    },
                new ProductColor {
                    ColorId = data.Colors.Where(c=>c.Name == "Grey").Select(c=>c.Id).FirstOrDefault(),
                    ProductId = data.Products.Where(p=>p.Dimensions == "10/10/6").Select(p=>p.Id).FirstOrDefault()
                    },
                new ProductColor {
                    ColorId = data.Colors.Where(c=>c.Name == "Washed Yellow").Select(c=>c.Id).FirstOrDefault(),
                    ProductId = data.Products.Where(p=>p.Dimensions == "10/10/6").Select(p=>p.Id).FirstOrDefault()
                    },
                 new ProductColor {
                    ColorId = data.Colors.Where(c=>c.Name == "Yellow").Select(c=>c.Id).FirstOrDefault(),
                    ProductId = data.Products.Where(p=>p.Dimensions == "40/20/20").Select(p=>p.Id).FirstOrDefault()
                    },
                 new ProductColor {
                    ColorId = data.Colors.Where(c=>c.Name == "Blue").Select(c=>c.Id).FirstOrDefault(),
                    ProductId = data.Products.Where(p=>p.Dimensions == "40/40/5").Select(p=>p.Id).FirstOrDefault()
                    },

            });

            data.SaveChanges();
        }

        private static void SeedRoles(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            if (roleManager.Roles.Any())
            {
                return;
            }

            Task
                .Run(async () =>
                {
                    await roleManager.CreateAsync(new IdentityRole("Administrator"));
                    await roleManager.CreateAsync(new IdentityRole("Employee"));
                    await roleManager.CreateAsync(new IdentityRole("Basic"));
                })
                .GetAwaiter()
                .GetResult();
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            if (userManager.Users.Any(u => u.UserName == "admin"))
            {
                return;
            }

            Task
                .Run(async () =>
                {
                    var role = await roleManager.FindByNameAsync(AdministratorRoleName);

                    const string adminUsername = "admin";
                    const string adminEmail = "admin@cps.com";
                    const string adminPassword = "admin12";

                    var user = new IdentityUser
                    {
                        UserName = adminUsername,
                        Email = adminEmail
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }

    }
}
