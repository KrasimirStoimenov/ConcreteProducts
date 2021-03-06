namespace ConcreteProducts.Services.Chats
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using ConcreteProducts.Data;
    using ConcreteProducts.Data.Models;
    using ConcreteProducts.Services.Chats.Models;
    using Microsoft.EntityFrameworkCore;

    public class ChatService : IChatService
    {
        private readonly ConcreteProductsDbContext data;
        private readonly IMapper mapper;

        public ChatService(ConcreteProductsDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public async Task CreateAsync(string text, string username, string userId)
        {
            var message = new ChatMessage
            {
                Text = text,
                UserId = userId,
                UserName = username,
                PublishedOn = DateTime.UtcNow,
            };

            await this.data.ChatMessages.AddAsync(message);
            await this.data.SaveChangesAsync();
        }

        public async Task<IEnumerable<MessageServiceModel>> GetAllMessagesAsync()
            => await this.data.ChatMessages
                .ProjectTo<MessageServiceModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();
    }
}
