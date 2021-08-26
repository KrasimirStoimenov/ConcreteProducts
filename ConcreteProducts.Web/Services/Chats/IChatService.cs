namespace ConcreteProducts.Web.Services.Chats
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ConcreteProducts.Web.Services.Chats.Models;

    public interface IChatService
    {
        Task CreateAsync(string text, string username, string userId);

        Task<IEnumerable<MessageServiceModel>> GetAllMessagesAsync();
    }
}
