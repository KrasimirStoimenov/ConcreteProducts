namespace ConcreteProducts.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using ConcreteProducts.Services.Chats;
    using System.Linq;

    [Authorize]
    public class ChatController : Controller
    {
        private readonly IChatService chatService;

        public ChatController(IChatService chatService)
            => this.chatService = chatService;

        public IActionResult Chat()
            => View(this.chatService.GetAllMessages());
    }
}
