﻿namespace ConcreteProducts.Web.Infrastructure
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using ConcreteProducts.Web.Data;
    using ConcreteProducts.Web.Data.Models;
    using ConcreteProducts.Web.Data.Models.Enumerations;

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
                new Color {Name = "Washed Red"},
                new Color {Name = "Yellow"},
                new Color {Name = "Washed Yellow"},
                new Color {Name = "White"},
                new Color {Name = "Washed White"},
                new Color {Name = "Blue"},
                new Color {Name = "Washed Blue"},
                new Color {Name = "Green"},
                new Color {Name = "Washed Green"},
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
                    Name = "Паве",
                    Dimensions = "10/10/6",
                    UnitOfMeasurement = UnitOfMeasurement.SquareMeters,
                    QuantityInPalletInUnitOfMeasurement = 9.36,
                    QuantityInPalletInPieces = 936,
                    CountInUnitOfMeasurement = 100,
                    Weight = 1.34,
                    ImageUrl = "https://www.masterhaus.bg/thumbs/246x246/products/11/03/11/1103110014-15-tuhla-beton_246x246_pad_478b24840a.jpg",
                    CategoryId = data.Categories.Where(c=>c.Name == "Pavement").Select(c=>c.Id).FirstOrDefault(),
                    },
                new Product {
                    Name = "Плочка",
                    Dimensions = "40/40/5",
                    UnitOfMeasurement = UnitOfMeasurement.SquareMeters,
                    QuantityInPalletInUnitOfMeasurement = 9.60,
                    QuantityInPalletInPieces = 60,
                    CountInUnitOfMeasurement = 6.25,
                    Weight = 16.67,
                    ImageUrl = "https://www.masterhaus.bg/thumbs/246x246/products/11/03/11/1103110014-15-tuhla-beton_246x246_pad_478b24840a.jpg",
                    CategoryId = data.Categories.Where(c=>c.Name == "Sidewalk").Select(c=>c.Id).FirstOrDefault(),
                    },
                new Product {
                    Name = "Двойно Т",
                    Dimensions = "20/16,5/6",
                    UnitOfMeasurement = UnitOfMeasurement.SquareMeters,
                    QuantityInPalletInUnitOfMeasurement = 9.29,
                    QuantityInPalletInPieces = 325,
                    CountInUnitOfMeasurement = 35,
                    Weight = 3.93,
                    ImageUrl = "https://www.masterhaus.bg/thumbs/246x246/products/11/03/11/1103110014-15-tuhla-beton_246x246_pad_478b24840a.jpg",
                    CategoryId = data.Categories.Where(c=>c.Name == "Flooring").Select(c=>c.Id).FirstOrDefault(),
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
