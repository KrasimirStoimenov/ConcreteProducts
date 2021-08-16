namespace ConcreteProducts.Data.Models
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

        public ICollection<WarehouseProductColors> WarehouseProducts { get; init; } = new HashSet<WarehouseProductColors>();

        public ICollection<Shape> Shapes { get; init; } = new HashSet<Shape>();
    }
}
