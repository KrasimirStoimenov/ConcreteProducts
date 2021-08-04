namespace ConcreteProducts.Web.Services.Products.Models
{
    using System.Collections.Generic;

    public class ProductDetailsServiceModel : ProductBaseServiceModel
    {
        public double QuantityInPalletInUnitOfMeasurement { get; init; }

        public double QuantityInPalletInPieces { get; init; }

        public double CountInUnitOfMeasurement { get; init; }

        public string UnitOfMeasurement { get; init; }

        public string ImageUrl { get; init; }

        public string CategoryName { get; init; }

        public double Weight { get; init; }

        public IEnumerable<string> AvailableColorsName { get; init; }
    }
}
