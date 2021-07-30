﻿namespace ConcreteProducts.Web.Areas.Admin.Models.Colors
{
    using System.Collections.Generic;
    using ConcreteProducts.Web.Models;
    using ConcreteProducts.Web.Services.Colors.Dtos;

    public class ListAllColorsViewModel : PagingViewModel
    {
        public IEnumerable<ColorServiceModel> AllColors { get; set; }
    }
}