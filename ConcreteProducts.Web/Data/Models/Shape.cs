namespace ConcreteProducts.Web.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Shape;

    public class Shape
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DimensionsMaxLength)]
        public string Dimensions { get; init; }

        public int WarehouseId { get; init; }

        public Warehouse Warehouse { get; init; }
    }
}
