namespace ConcreteProducts.Web.Services.Chats.Models
{
    using System;

    public class MessageServiceModel
    {
        public string Text { get; init; }

        public string UserId { get; init; }

        public string Username { get; init; }

        public DateTime PublishedOn { get; init; }
    }
}
