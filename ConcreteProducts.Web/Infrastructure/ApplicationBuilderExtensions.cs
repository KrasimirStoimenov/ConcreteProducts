namespace ConcreteProducts.Web.Infrastructure
{
    using System.Linq;
    using ConcreteProducts.Web.Data;
    using ConcreteProducts.Web.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder ApplyMigrations(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();

            var dbContext = services.ServiceProvider.GetService<ConcreteProductsDbContext>();

            dbContext.Database.Migrate();

            SeedColors(dbContext);
            SeedCategories(dbContext);    

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


    }
}
