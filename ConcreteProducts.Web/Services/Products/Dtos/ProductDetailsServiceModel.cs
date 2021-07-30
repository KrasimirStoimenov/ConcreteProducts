namespace ConcreteProducts.Web.Services.Products.Dtos
{
    using System.Collections.Generic;

    public class ProductDetailsServiceModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string Dimensions { get; init; }

        public double QuantityInPalletInUnitOfMeasurement { get; init; }

        public double QuantityInPalletInPieces { get; init; }

        public double CountInUnitOfMeasurement { get; init; }

        public string UnitOfMeasurement { get; init; }

        public string DefaultImageUrl { get; init; }

        public string CategoryName { get; init; }

        public double Weight { get; init; }

        public IEnumerable<string> AvailableColorsName { get; init; }
    }
}
