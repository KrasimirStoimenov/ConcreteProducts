namespace ConcreteProducts.Web.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Warehouse;

    public class Warehouse
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public ICollection<WarehouseProducts> WarehouseProducts { get; init; } = new HashSet<WarehouseProducts>();

        public ICollection<Shape> Shapes { get; init; } = new HashSet<Shape>();
    }
}
