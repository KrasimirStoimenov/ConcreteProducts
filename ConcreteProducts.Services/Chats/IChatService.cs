namespace ConcreteProducts.Services.Chats
{
    using System.Collections.Generic;

    using ConcreteProducts.Services.Chats.Models;

    public interface IChatService
    {
        void Create(string text, string username, string userId);

        IEnumerable<MessageServiceModel> GetAllMessages();
    }
}
