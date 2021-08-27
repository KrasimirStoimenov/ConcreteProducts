namespace ConcreteProducts.Services.Colors
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;

    using ConcreteProducts.Data;
    using ConcreteProducts.Data.Models;
    using ConcreteProducts.Services.Colors.Models;

    public class ColorService : IColorService
    {
        private readonly ConcreteProductsDbContext data;
        private readonly IMapper mapper;

        public ColorService(ConcreteProductsDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ColorBaseServiceModel>> GetAllColorsAsync()
            => await this.data.Colors
                .ProjectTo<ColorBaseServiceModel>(this.mapper.ConfigurationProvider)
                .OrderBy(c => c.Name)
                .ToListAsync();

        public async Task<ColorDeleteServiceModel> GetColorToDeleteByIdAsync(int id)
            => await this.data.Colors
                .Where(c => c.Id == id)
                .ProjectTo<ColorDeleteServiceModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

        public async Task<int> CreateAsync(string name)
        {
            var color = new Color
            {
                Name = name
            };

            await this.data.Colors.AddAsync(color);
            await this.data.SaveChangesAsync();

            return color.Id;
        }

        public async Task EditAsync(int id, string name)
        {
            var color = await this.data.Colors.FindAsync(id);

            color.Name = name;

            await this.data.SaveChangesAsync();
        }

        public async Task<ColorBaseServiceModel> GetColorDetailsAsync(int id)
            => await this.data.Colors
                .Where(c => c.Id == id)
                .ProjectTo<ColorBaseServiceModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

        public bool IsColorExist(int id)
            => this.data.Colors.Any(c => c.Id == id);

        public async Task<bool> IsColorExistAsync(int id)
            => await this.data.Colors.AnyAsync(c => c.Id == id);

        public async Task<bool> HasColorWithSameNameAsync(string name)
            => await this.data.Colors
                .AnyAsync(c => c.Name == name);

        public async Task DeleteColorAsync(int id)
        {
            var color = await this.data.Colors.FindAsync(id);

            this.data.Colors.Remove(color);
            await this.data.SaveChangesAsync();
        }
    }
}
