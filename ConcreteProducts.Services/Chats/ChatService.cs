namespace ConcreteProducts.Services.Chats
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using ConcreteProducts.Data;
    using ConcreteProducts.Data.Models;
    using ConcreteProducts.Services.Chats.Models;

    public class ChatService : IChatService
    {
        private readonly ConcreteProductsDbContext data;
        private readonly IMapper mapper;

        public ChatService(ConcreteProductsDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public void Create(string text, string username, string userId)
        {
            var message = new ChatMessage
            {
                Text = text,
                UserId = userId,
                UserName = username,
                PublishedOn = DateTime.UtcNow,
            };

            this.data.ChatMessages.Add(message);
            this.data.SaveChanges();
        }

        public IEnumerable<MessageServiceModel> GetAllMessages()
            => this.data.ChatMessages
                .ProjectTo<MessageServiceModel>(this.mapper.ConfigurationProvider)
                .ToList();
    }
}
