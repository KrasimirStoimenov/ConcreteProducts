namespace ConcreteProducts.Web.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Color
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(ColorNameMaxLength)]
        public string Name { get; set; }

        public ICollection<ProductColor> ProductColors { get; init; } = new HashSet<ProductColor>();
    }
}
