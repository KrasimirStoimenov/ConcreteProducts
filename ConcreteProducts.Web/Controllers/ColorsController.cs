﻿namespace ConcreteProducts.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ConcreteProducts.Web.Data;
    using ConcreteProducts.Web.Models.Colors;
    using ConcreteProducts.Web.Data.Models;
    using System.Linq;
    using ConcreteProducts.Web.Services.Colors;

    public class ColorsController : Controller
    {
        private readonly IColorService colorService;
        private readonly ConcreteProductsDbContext data;

        public ColorsController(IColorService colorService, ConcreteProductsDbContext data)
        {
            this.colorService = colorService;
            this.data = data;
        }

        public IActionResult All(int id = 1)
        {
            const int itemsPerPage = 8;

            var colors = colorService.GetAllColors();

            var colorsViewModel = new ListAllColorsViewModel
            {
                AllColors = colors.Skip((id - 1) * itemsPerPage).Take(itemsPerPage),
                PageNumber = id,
                Count = colors.Count(),
                ItemsPerPage = itemsPerPage
            };

            return View(colorsViewModel);
        }

        public IActionResult Add()
            => View(new AddColorFormModel());

        [HttpPost]
        public IActionResult Add(AddColorFormModel color)
        {
            if (!ModelState.IsValid)
            {
                return View(color);
            }

            var currentColor = new Color
            {
                Name = color.Name
            };

            this.data.Colors.Add(currentColor);
            this.data.SaveChanges();

            return RedirectToAction("All");
        }

        public IActionResult Delete(int id)
        {
            if (!this.colorService.IsColorExist(id))
            {
                return BadRequest("Color does not exist.");
            }

            this.colorService.DeleteColor(id);

            return RedirectToAction(nameof(All));
        }
    }
}
