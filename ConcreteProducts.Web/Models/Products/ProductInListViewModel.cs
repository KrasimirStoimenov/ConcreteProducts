namespace ConcreteProducts.Web.Models.Products
{
    using System.Collections.Generic;
    using ConcreteProducts.Web.Data.Models.Enumerations;

    public class ProductInListViewModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string Dimensions { get; init; }

        public string InPallet { get; init; }

        public string DefaultImageUrl { get; init; }
    }
}
