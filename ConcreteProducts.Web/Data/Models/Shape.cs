﻿namespace ConcreteProducts.Web.Data.Models
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
        public string Dimensions { get; set; }

        public int WarehouseId { get; set; }

        public Warehouse Warehouse { get; init; }
    }
}
