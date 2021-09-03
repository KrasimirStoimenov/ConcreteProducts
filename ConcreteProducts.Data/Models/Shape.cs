namespace ConcreteProducts.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static ConcreteProducts.Common.DataAttributeConstants.Shape;

    public class Shape
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DimensionsMaxLength)]
        public string Dimensions { get; set; }

        public int WarehouseId { get; set; }

        public Warehouse Warehouse { get; init; }

        public IEnumerable<ShapeHistory> Histories { get; set; }
    }
}
