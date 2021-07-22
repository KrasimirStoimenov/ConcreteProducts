namespace ConcreteProducts.Web.Services.Products.Dtos
{
    public class ProductServiceModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string Dimensions { get; init; }

        public string InPallet { get; init; }

        public string DefaultImageUrl { get; init; }

        public string CategoryName { get; init; }
    }
}
