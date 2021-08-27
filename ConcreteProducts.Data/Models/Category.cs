﻿namespace ConcreteProducts.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Common.DataAttributeConstants.Category;

    public class Category
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public ICollection<Product> Products { get; init; } = new HashSet<Product>();
    }
}
