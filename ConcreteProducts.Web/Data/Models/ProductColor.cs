﻿namespace ConcreteProducts.Web.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ProductColor
    {
        public int ProductColorId { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; init; }

        public int ColorId { get; set; }
        public Color Color { get; init; }

        public IEnumerable<WarehouseProductColors> Warehouses { get; init; } = new HashSet<WarehouseProductColors>();
    }
}
