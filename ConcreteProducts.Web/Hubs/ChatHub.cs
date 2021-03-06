namespace ConcreteProducts.Web.Hubs
{
    using System;
    using System.Threading.Tasks;

    using ConcreteProducts.Services.Chats;
    using ConcreteProducts.Services.Chats.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.SignalR;

    using static ConcreteProducts.Common.DataAttributeConstants.ChatMessages;

    public class ChatHub : Hub
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IChatService chatService;

        public ChatHub(UserManager<IdentityUser> userManager, IChatService chatService)
        {
            this.userManager = userManager;
            this.chatService = chatService;
        }

        public async Task Send(string message)
        {
            if (!this.IsValidMessageLength(message))
            {
                return;
            }

            var user = await this.userManager.GetUserAsync(this.Context.User);

            await this.chatService.CreateAsync(message, user.UserName, user.Id);

            await this.Clients.All.SendAsync(
                "ReceiveMessage",
                new MessageServiceModel
                {
                    Text = message,
                    Username = this.Context.User.Identity.Name,
                    PublishedOn = DateTime.UtcNow,
                });
        }

        private bool IsValidMessageLength(string message)
            => message.Length <= MessageTextMaxLength
            && message.Length >= MessageTextMinLength;
    }
}
