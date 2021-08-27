namespace ConcreteProducts.Web.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using ConcreteProducts.Services.Colors;
    using ConcreteProducts.Web.Areas.Admin.Models.Colors;

    using static Common.GlobalConstants;

    public class ColorsController : AdminController
    {
        private readonly string notExistingColorErrorMessage = "Color does not exist.";
        private readonly string takenColorNameErrorMessage = "Color name already taken.";

        private readonly IColorService colorService;

        public ColorsController(IColorService colorService)
        {
            this.colorService = colorService;
        }

        public async Task<IActionResult> All(int page = 1)
        {
            var colors = await colorService.GetAllColorsAsync();

            var colorsViewModel = new ListAllColorsViewModel
            {
                AllColors = colors
                    .OrderBy(c => c.Name)
                    .Skip((page - 1) * ItemsPerPage)
                    .Take(ItemsPerPage),
                PageNumber = page,
                Count = colors.Count(),
                ItemsPerPage = ItemsPerPage
            };

            return View(colorsViewModel);
        }

        public IActionResult Add()
            => View(new ColorFormModel());

        [HttpPost]
        public async Task<IActionResult> Add(ColorFormModel color)
        {
            if (await this.colorService.HasColorWithSameNameAsync(color.Name))
            {
                this.ModelState.AddModelError(nameof(color.Name), takenColorNameErrorMessage);
            }

            if (!ModelState.IsValid)
            {
                return View(color);
            }

            await this.colorService.CreateAsync(color.Name);

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!await this.colorService.IsColorExistAsync(id))
            {
                return BadRequest(notExistingColorErrorMessage);
            }

            var colorDetails = await this.colorService.GetColorDetailsAsync(id);

            return View(new ColorFormModel
            {
                Name = colorDetails.Name
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ColorFormModel color)
        {
            if (await this.colorService.HasColorWithSameNameAsync(color.Name))
            {
                this.ModelState.AddModelError(nameof(color.Name), takenColorNameErrorMessage);
            }

            if (!ModelState.IsValid)
            {
                return View(color);
            }

            await this.colorService.EditAsync(color.Id, color.Name);

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!await this.colorService.IsColorExistAsync(id))
            {
                return BadRequest(notExistingColorErrorMessage);
            }

            var color = await this.colorService.GetColorToDeleteByIdAsync(id);

            return View(color);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.colorService.DeleteColorAsync(id);

            return RedirectToAction(nameof(All));
        }
    }
}
