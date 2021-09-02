namespace ConcreteProducts.Web.Controllers
{
    using System.Threading.Tasks;

    using ConcreteProducts.Services.Chats;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class ChatController : Controller
    {
        private readonly IChatService chatService;

        public ChatController(IChatService chatService)
            => this.chatService = chatService;

        public async Task<IActionResult> Chat()
            => this.View(await this.chatService.GetAllMessagesAsync());
    }
}
