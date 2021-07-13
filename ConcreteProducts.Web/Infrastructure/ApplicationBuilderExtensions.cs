namespace ConcreteProducts.Web.Infrastructure
{
    using System.Linq;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using ConcreteProducts.Web.Data;
    using ConcreteProducts.Web.Data.Models;
    using ConcreteProducts.Web.Data.Models.Enumerations;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder ApplyMigrations(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();

            var dbContext = services.ServiceProvider.GetService<ConcreteProductsDbContext>();

            dbContext.Database.Migrate();

            SeedColors(dbContext);
            SeedCategories(dbContext);
            SeedProducts(dbContext);
            SeedProductColor(dbContext);

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

        private static void SeedProducts(ConcreteProductsDbContext data)
        {
            if (data.Products.Any())
            {
                return;
            }

            data.Products.AddRange(new[]
            {
                new Product {
                    Name = "10/10/6",
                    UnitOfMeasurement = UnitOfMeasurement.SquareMeters,
                    QuantityInPalletInUnitOfMeasurement = 9.36,
                    QuantityInPalletInPieces = 936,
                    CountInUnitOfMeasurement = 100,
                    Weight = 1.34,
                    CategoryId = data.Categories.Where(c=>c.Name == "Pavement").Select(c=>c.Id).FirstOrDefault()
                    },
                new Product {
                    Name = "40/40/5",
                    UnitOfMeasurement = UnitOfMeasurement.SquareMeters,
                    QuantityInPalletInUnitOfMeasurement = 9.60,
                    QuantityInPalletInPieces = 60,
                    CountInUnitOfMeasurement = 6.25,
                    Weight = 16.67,
                    CategoryId = data.Categories.Where(c=>c.Name == "Sidewalk").Select(c=>c.Id).FirstOrDefault(),
                    ImageUrl = "https://sd.cd9.xyz/image/4022-plochka-trotoarna-gladka-40405-siva-original-449.jpg"
                    },
                new Product {
                    Name = "20/16,5/6",
                    UnitOfMeasurement = UnitOfMeasurement.SquareMeters,
                    QuantityInPalletInUnitOfMeasurement = 9.29,
                    QuantityInPalletInPieces = 325,
                    CountInUnitOfMeasurement = 35,
                    Weight = 3.93,
                    CategoryId = data.Categories.Where(c=>c.Name == "Flooring").Select(c=>c.Id).FirstOrDefault(),
                    ImageUrl = "https://osnovi.com/media/7/1404.jpg"
                    },
                new Product {
                    Name = "50/15/25",
                    UnitOfMeasurement = UnitOfMeasurement.Meters,
                    QuantityInPalletInUnitOfMeasurement = 18,
                    QuantityInPalletInPieces = 36,
                    CountInUnitOfMeasurement = 2,
                    Weight = 36.11,
                    CategoryId = data.Categories.Where(c=>c.Name == "Border").Select(c=>c.Id).FirstOrDefault(),
                    ImageUrl = "https://www.reliks-vibro.com/uploads/media/stenik_products_gallery/0001/03/05a2ebb0a2a5701bd5d52ad19339b1e7e9f25bae.jpeg"
                    },
                new Product {
                    Name = "40/20/20",
                    UnitOfMeasurement = UnitOfMeasurement.SquareMeters,
                    QuantityInPalletInUnitOfMeasurement = 3.84,
                    QuantityInPalletInPieces = 48,
                    CountInUnitOfMeasurement = 12.5,
                    Weight = 25,
                    CategoryId = data.Categories.Where(c=>c.Name == "Brick").Select(c=>c.Id).FirstOrDefault(),
                    ImageUrl = "https://ikoen.bg/assets/pages/small/e66a5c859d95bdec268f38a22904eaa8.jpg?rand=7%0D%0A"
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
                    ProductId = data.Products.Where(p=>p.Name == "10/10/6").Select(p=>p.Id).FirstOrDefault()
                    },
                new ProductColor {
                    ColorId = data.Colors.Where(c=>c.Name == "Grey").Select(c=>c.Id).FirstOrDefault(),
                    ProductId = data.Products.Where(p=>p.Name == "10/10/6").Select(p=>p.Id).FirstOrDefault()
                    },
                new ProductColor {
                    ColorId = data.Colors.Where(c=>c.Name == "Washed Yellow").Select(c=>c.Id).FirstOrDefault(),
                    ProductId = data.Products.Where(p=>p.Name == "10/10/6").Select(p=>p.Id).FirstOrDefault()
                    },
                 new ProductColor {
                    ColorId = data.Colors.Where(c=>c.Name == "Yellow").Select(c=>c.Id).FirstOrDefault(),
                    ProductId = data.Products.Where(p=>p.Name == "40/20/20").Select(p=>p.Id).FirstOrDefault()
                    },
                 new ProductColor {
                    ColorId = data.Colors.Where(c=>c.Name == "Blue").Select(c=>c.Id).FirstOrDefault(),
                    ProductId = data.Products.Where(p=>p.Name == "40/40/5").Select(p=>p.Id).FirstOrDefault()
                    },

            });

            data.SaveChanges();
        }

    }
}
