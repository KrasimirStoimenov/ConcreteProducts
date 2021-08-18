namespace ConcreteProducts.Web.Services.Chats
{
    using System.Collections.Generic;

    using ConcreteProducts.Web.Services.Chats.Models;

    public interface IChatService
    {
        void Create(string text, string username, string userId);

        IEnumerable<MessageServiceModel> GetAllMessages();
    }
}
