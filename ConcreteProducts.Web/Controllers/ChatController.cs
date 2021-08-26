namespace ConcreteProducts.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using ConcreteProducts.Services.Chats;

    [Authorize]
    public class ChatController : Controller
    {
        private readonly IChatService chatService;

        public ChatController(IChatService chatService)
            => this.chatService = chatService;

        public async Task<IActionResult> Chat()
            => View(await this.chatService.GetAllMessagesAsync());
    }
}
