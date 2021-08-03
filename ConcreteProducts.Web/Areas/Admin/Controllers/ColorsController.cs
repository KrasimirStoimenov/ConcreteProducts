namespace ConcreteProducts.Web.Areas.Admin.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using ConcreteProducts.Web.Services.Colors;
    using ConcreteProducts.Web.Areas.Admin.Models.Colors;

    public class ColorsController : AdminController
    {
        private readonly string notExistingColorErrorMessage = "Color does not exist.";
        private readonly string takenColorNameErrorMessage = "Color name already taken.";
        private readonly IColorService colorService;

        public ColorsController(IColorService colorService)
        {
            this.colorService = colorService;
        }

        public IActionResult All(int page = 1)
        {
            const int itemsPerPage = 8;

            var colors = colorService.GetAllColors();

            var colorsViewModel = new ListAllColorsViewModel
            {
                AllColors = colors.Skip((page - 1) * itemsPerPage).Take(itemsPerPage),
                PageNumber = page,
                Count = colors.Count(),
                ItemsPerPage = itemsPerPage
            };

            return View(colorsViewModel);
        }

        public IActionResult Add()
            => View(new ColorFormModel());

        [HttpPost]
        public IActionResult Add(ColorFormModel color)
        {
            if (this.colorService.HasColorWithSameName(color.Name))
            {
                this.ModelState.AddModelError(nameof(color.Name), takenColorNameErrorMessage);
            }

            if (!ModelState.IsValid)
            {
                return View(color);
            }

            this.colorService.Create(color.Name);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Edit(int id)
        {
            if (!this.colorService.IsColorExist(id))
            {
                return BadRequest(notExistingColorErrorMessage);
            }

            var colorDetails = this.colorService.GetColorDetails(id);

            return View(new ColorFormModel
            {
                Name = colorDetails.Name
            });
        }

        [HttpPost]
        public IActionResult Edit(int id, ColorFormModel color)
        {
            if (!this.colorService.IsColorExist(id))
            {
                this.ModelState.AddModelError(nameof(color.Name), notExistingColorErrorMessage);
            }

            if (this.colorService.HasColorWithSameName(color.Name))
            {
                this.ModelState.AddModelError(nameof(color.Name), takenColorNameErrorMessage);
            }

            if (!ModelState.IsValid)
            {
                return View(color);
            }

            this.colorService.Edit(id, color.Name);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Delete(int id)
        {
            if (!this.colorService.IsColorExist(id))
            {
                return BadRequest(notExistingColorErrorMessage);
            }

            var color = this.colorService.GetColorToDeleteById(id);

            return View(color);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            this.colorService.DeleteColor(id);

            return RedirectToAction(nameof(All));
        }
    }
}
