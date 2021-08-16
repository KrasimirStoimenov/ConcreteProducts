namespace ConcreteProducts.Web.Models.Chat
{
    using System;

    public class MessageViewModel
    {
        public string Text { get; init; }

        public string Username { get; init; }

        public DateTime PublishedOn { get; init; }
    }
}
