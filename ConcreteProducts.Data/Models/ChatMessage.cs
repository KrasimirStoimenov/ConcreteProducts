namespace ConcreteProducts.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static ConcreteProducts.Common.DataAttributeConstants.ChatMessages;

    public class ChatMessage
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(MessageTextMaxLength)]
        public string Text { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        public DateTime PublishedOn { get; set; }
    }
}
