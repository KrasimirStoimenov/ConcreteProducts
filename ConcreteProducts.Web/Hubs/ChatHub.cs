namespace ConcreteProducts.Web.Hubs
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;

    using ConcreteProducts.Web.Models.Chat;

    public class ChatHub : Hub
    {
        public async Task Send(string message)
        {
            await this.Clients.All.SendAsync(
                "ReceiveMessage",
                new MessageViewModel
                {
                    Text = message,
                    Username = this.Context.User.Identity.Name,
                    PublishedOn = DateTime.UtcNow,
                });
        }
    }
}
