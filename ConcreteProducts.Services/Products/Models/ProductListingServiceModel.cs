namespace ConcreteProducts.Services.Products.Models
{
    public class ProductListingServiceModel : ProductBaseServiceModel
    {
        public string InPallet { get; init; }

        public string ImageUrl { get; init; }

        public string CategoryName { get; init; }
    }
}
