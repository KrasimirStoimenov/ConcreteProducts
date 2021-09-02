namespace ConcreteProducts.Web.Infrastructure.Extensions
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using ConcreteProducts.Data;
    using ConcreteProducts.Data.Models;
    using ConcreteProducts.Data.Models.Enumerations;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using static Common.GlobalConstants;

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
            SeedShapes(dbContext);
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
                new Color {Name = "White"},
                new Color {Name = "Washed White"},
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
                new Warehouse {Name = "Basic"},
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
                    Name = "Tile",
                    Dimensions = "40/40/5",
                    UnitOfMeasurement = UnitOfMeasurement.SquareMeters,
                    QuantityInPalletInUnitOfMeasurement = 9.6,
                    QuantityInPalletInPieces = 60,
                    CountInUnitOfMeasurement = 6.25,
                    Weight = 16.67,
                    ImageUrl = "https://www.masterhaus.bg/files/products/10/03/02/1003020010-2.jpg",
                    CategoryId = data.Categories.Where(c=>c.Name == "Sidewalk").Select(c=>c.Id).FirstOrDefault(),
                    },
                new Product {
                    Name = "Tile",
                    Dimensions = "55/40/5",
                    UnitOfMeasurement = UnitOfMeasurement.SquareMeters,
                    QuantityInPalletInUnitOfMeasurement = 12.30,
                    QuantityInPalletInPieces = 56,
                    CountInUnitOfMeasurement = 4.50,
                    Weight = 21.43,
                    ImageUrl = "https://www.geoton.com/uploads/products/thumb_plocha_55-40-5.jpg",
                    CategoryId = data.Categories.Where(c=>c.Name == "Sidewalk").Select(c=>c.Id).FirstOrDefault(),
                    },
                new Product {
                    Name = "Honeycomb",
                    Dimensions = "30/30/6",
                    UnitOfMeasurement = UnitOfMeasurement.SquareMeters,
                    QuantityInPalletInUnitOfMeasurement = 7.09,
                    QuantityInPalletInPieces = 78,
                    CountInUnitOfMeasurement = 11,
                    Weight = 13.27,
                    ImageUrl = "https://www.reliks-vibro.com/uploads/media/stenik_products_gallery/0001/01/0ab7d205a50146e20497d4e204165ac21474bd54.jpeg",
                    CategoryId = data.Categories.Where(c=>c.Name == "Flooring").Select(c=>c.Id).FirstOrDefault(),
                    },
                new Product {
                    Name = "Behaton",
                    Dimensions = "20/16,5/6",
                    UnitOfMeasurement = UnitOfMeasurement.SquareMeters,
                    QuantityInPalletInUnitOfMeasurement = 9.29,
                    QuantityInPalletInPieces = 325,
                    CountInUnitOfMeasurement = 35,
                    Weight = 3.92,
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTO2fu8gAR8qIkB2EMPD5AMRmZFeS6aH-FvYQ&usqp=CAU",
                    CategoryId = data.Categories.Where(c=>c.Name == "Flooring").Select(c=>c.Id).FirstOrDefault(),
                    },
                new Product {
                    Name = "Border",
                    Dimensions = "50/8/16",
                    UnitOfMeasurement = UnitOfMeasurement.Meters,
                    QuantityInPalletInUnitOfMeasurement = 50,
                    QuantityInPalletInPieces = 100,
                    CountInUnitOfMeasurement = 2,
                    Weight = 13.21,
                    ImageUrl = "https://cdn5.focus.bg/bazar//73/pics/73f71ddd37901b3af94711926c11742b.jpg",
                    CategoryId = data.Categories.Where(c=>c.Name == "Border").Select(c=>c.Id).FirstOrDefault(),
                    },
                new Product {
                    Name = "Border",
                    Dimensions = "50/15/25",
                    UnitOfMeasurement = UnitOfMeasurement.Meters,
                    QuantityInPalletInUnitOfMeasurement = 18,
                    QuantityInPalletInPieces = 36,
                    CountInUnitOfMeasurement = 2,
                    Weight = 36.11,
                    ImageUrl = "https://cdncloudcart.com/13688/products/images/27598/bordur-501525-15-semmelrock-image_5f06bb847533f_600x600.jpeg?1594276758",
                    CategoryId = data.Categories.Where(c=>c.Name == "Border").Select(c=>c.Id).FirstOrDefault(),
                    },
                new Product {
                    Name = "Brick",
                    Dimensions = "25/12,5/6",
                    UnitOfMeasurement = UnitOfMeasurement.SquareMeters,
                    QuantityInPalletInUnitOfMeasurement = 9.75,
                    QuantityInPalletInPieces = 312,
                    CountInUnitOfMeasurement = 66.6,
                    Weight = 4.00,
                    ImageUrl = "https://tuhlibg.com/wp-content/uploads/2017/07/%D0%A2%D1%83%D1%85%D0%BB%D0%B0-%D0%B7%D0%B0-%D0%B7%D0%B8%D0%B4%D0%B0%D0%BD%D0%B5.jpg",
                    CategoryId = data.Categories.Where(c=>c.Name == "Brick").Select(c=>c.Id).FirstOrDefault(),
                    },
                new Product {
                    Name = "Brick",
                    Dimensions = "40/15/20",
                    UnitOfMeasurement = UnitOfMeasurement.SquareMeters,
                    QuantityInPalletInUnitOfMeasurement = 4.80,
                    QuantityInPalletInPieces = 60,
                    CountInUnitOfMeasurement = 12.50,
                    Weight = 15.00,
                    ImageUrl = "https://cdncloudcart.com/11354/products/images/36954/tuhla-6-ca-betonova-siva-40-x-15-x-19-5-cm-image_605c8f9f62847_600x600.jpeg?1616678835",
                    CategoryId = data.Categories.Where(c=>c.Name == "Brick").Select(c=>c.Id).FirstOrDefault(),
                    },
                new Product {
                    Name = "Brick",
                    Dimensions = "40/20/20",
                    UnitOfMeasurement = UnitOfMeasurement.SquareMeters,
                    QuantityInPalletInUnitOfMeasurement = 3.84,
                    QuantityInPalletInPieces = 48,
                    CountInUnitOfMeasurement = 12.50,
                    Weight = 25.00,
                    ImageUrl = "https://davitekbetonstroy.com/wp-content/uploads/2020/04/3-split-bez-jleb.jpg",
                    CategoryId = data.Categories.Where(c=>c.Name == "Brick").Select(c=>c.Id).FirstOrDefault(),
                    },
            });

            data.SaveChanges();
        }

        private static void SeedShapes(ConcreteProductsDbContext data)
        {
            if (data.Shapes.Any())
            {
                return;
            }

            data.Shapes.AddRange(new[]
            {
                new Shape {
                    Name = "Pave",
                    Dimensions = "10/10/6",
                    WarehouseId = data.Warehouses.Where(c=>c.Name == "Basic").Select(c=>c.Id).FirstOrDefault(),
                    },
                new Shape {
                    Name = "Pave",
                    Dimensions = "20/20/6",
                    WarehouseId = data.Warehouses.Where(c=>c.Name == "Basic").Select(c=>c.Id).FirstOrDefault(),
                    },
                new Shape {
                    Name = "Pave",
                    Dimensions = "20/10/6",
                    WarehouseId = data.Warehouses.Where(c=>c.Name == "Basic").Select(c=>c.Id).FirstOrDefault(),
                    },
                new Shape {
                    Name = "Tile",
                    Dimensions = "30/30/6",
                    WarehouseId = data.Warehouses.Where(c=>c.Name == "Basic").Select(c=>c.Id).FirstOrDefault(),
                    },
                new Shape {
                    Name = "Tile",
                    Dimensions = "40/40/5",
                    WarehouseId = data.Warehouses.Where(c=>c.Name == "Basic").Select(c=>c.Id).FirstOrDefault(),
                    },
                new Shape {
                    Name = "Tile",
                    Dimensions = "55/40/5",
                    WarehouseId = data.Warehouses.Where(c=>c.Name == "Basic").Select(c=>c.Id).FirstOrDefault(),
                    },
                new Shape {
                    Name = "Honeycomb",
                    Dimensions = "30/30/6",
                    WarehouseId = data.Warehouses.Where(c=>c.Name == "Basic").Select(c=>c.Id).FirstOrDefault(),
                    },
                new Shape {
                    Name = "Behaton",
                    Dimensions = "20/16,5/6",
                    WarehouseId = data.Warehouses.Where(c=>c.Name == "Basic").Select(c=>c.Id).FirstOrDefault(),
                    },
                new Shape {
                    Name = "Border",
                    Dimensions = "50/8/16",
                    WarehouseId = data.Warehouses.Where(c=>c.Name == "Basic").Select(c=>c.Id).FirstOrDefault(),
                    },
                new Shape {
                    Name = "Border",
                    Dimensions = "50/15/25",
                    WarehouseId = data.Warehouses.Where(c=>c.Name == "Basic").Select(c=>c.Id).FirstOrDefault(),
                    },
                new Shape {
                    Name = "Brick",
                    Dimensions = "25/12,5/6",
                    WarehouseId = data.Warehouses.Where(c=>c.Name == "Basic").Select(c=>c.Id).FirstOrDefault(),
                    },
                new Shape {
                    Name = "Brick",
                    Dimensions = "40/15/20",
                    WarehouseId = data.Warehouses.Where(c=>c.Name == "Basic").Select(c=>c.Id).FirstOrDefault(),
                    },
                new Shape {
                    Name = "Brick",
                    Dimensions = "40/20/20",
                    WarehouseId = data.Warehouses.Where(c=>c.Name == "Basic").Select(c=>c.Id).FirstOrDefault(),
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
                        Email = adminEmail,
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
