namespace ConcreteProducts.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Common.DataAttributeConstants.Color;

    public class Color
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public ICollection<ProductColor> ProductColors { get; init; } = new HashSet<ProductColor>();
    }
}
