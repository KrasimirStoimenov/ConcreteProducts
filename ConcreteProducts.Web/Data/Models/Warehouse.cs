namespace ConcreteProducts.Web.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Warehouse
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(WarehouseNameMaxLength)]
        public string Name { get; set; }

        public ICollection<Product> Products { get; init; } = new HashSet<Product>();

        public ICollection<Shape> Shapes { get; init; } = new HashSet<Shape>();
    }
}
