﻿namespace ConcreteProducts.Web.Services.Colors
{
    using System.Collections.Generic;
    using ConcreteProducts.Web.Services.Colors.Models;

    public interface IColorService
    {
        IEnumerable<ColorBaseServiceModel> GetAllColors();

        ColorDeleteServiceModel GetColorToDeleteById(int id);

        int Create(string name);

        void Edit(int id, string name);

        ColorBaseServiceModel GetColorDetails(int id);

        bool IsColorExist(int id);

        bool HasColorWithSameName(string name);

        void DeleteColor(int id);
    }
}
