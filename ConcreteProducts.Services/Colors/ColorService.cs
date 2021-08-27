﻿namespace ConcreteProducts.Services.Colors
{
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

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

        public IEnumerable<ColorBaseServiceModel> GetAllColors()
            => this.data.Colors
                .ProjectTo<ColorBaseServiceModel>(this.mapper.ConfigurationProvider)
                .OrderBy(c => c.Name)
                .ToList();

        public ColorDeleteServiceModel GetColorToDeleteById(int id)
            => this.data.Colors
                .Where(c => c.Id == id)
                .ProjectTo<ColorDeleteServiceModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefault();

        public int Create(string name)
        {
            var color = new Color
            {
                Name = name
            };

            this.data.Colors.Add(color);
            this.data.SaveChanges();

            return color.Id;
        }

        public void Edit(int id, string name)
        {
            var color = this.data.Colors.Find(id);

            color.Name = name;

            this.data.SaveChanges();
        }

        public ColorBaseServiceModel GetColorDetails(int id)
            => this.data.Colors
                .Where(c => c.Id == id)
                .ProjectTo<ColorBaseServiceModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefault();

        public bool IsColorExist(int id)
            => this.data.Colors.Any(c => c.Id == id);

        public bool HasColorWithSameName(string name)
            => this.data.Colors
                .Any(c => c.Name == name);

        public void DeleteColor(int id)
        {
            var color = this.data.Colors.Find(id);

            this.data.Colors.Remove(color);
            this.data.SaveChanges();
        }
    }
}