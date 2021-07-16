namespace ConcreteProducts.Web.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Shape
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(ShapeNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(ShapeDimensionsMaxLength)]
        public string Dimensions { get; init; }

        public int WarehouseId { get; init; }

        public Warehouse Warehouse { get; init; }
    }
}
