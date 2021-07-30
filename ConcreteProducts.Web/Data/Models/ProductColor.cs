namespace ConcreteProducts.Web.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ProductColor
    {
        public int ProductId { get; set; }
        public Product Product { get; init; }

        public int ColorId { get; set; }
        public Color Color { get; init; }
    }
}
