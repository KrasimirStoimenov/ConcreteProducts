namespace ConcreteProducts.Web.Models.Categories
{
    public class CategoryListingModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public int ProductsCount { get; set; }
    }
}
