namespace ConcreteProducts.Web.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using ConcreteProducts.Services.Colors;
    using ConcreteProducts.Web.Areas.Admin.Models.Colors;
    using Microsoft.AspNetCore.Mvc;

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
            var colors = await this.colorService.GetAllColorsAsync();

            var colorsViewModel = new ListAllColorsViewModel
            {
                AllColors = colors
                    .OrderBy(c => c.Name)
                    .Skip((page - 1) * ItemsPerPage)
                    .Take(ItemsPerPage),
                PageNumber = page,
                Count = colors.Count(),
                ItemsPerPage = ItemsPerPage,
            };

            return this.View(colorsViewModel);
        }

        public IActionResult Add()
            => this.View(new ColorFormModel());

        [HttpPost]
        public async Task<IActionResult> Add(ColorFormModel color)
        {
            if (await this.colorService.HasColorWithSameNameAsync(color.Name))
            {
                this.ModelState.AddModelError(nameof(color.Name), this.takenColorNameErrorMessage);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(color);
            }

            await this.colorService.CreateAsync(color.Name);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!await this.colorService.IsColorExistAsync(id))
            {
                return this.BadRequest(this.notExistingColorErrorMessage);
            }

            var colorDetails = await this.colorService.GetColorDetailsAsync(id);

            return this.View(new ColorFormModel
            {
                Name = colorDetails.Name,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ColorFormModel color)
        {
            if (await this.colorService.HasColorWithSameNameAsync(color.Name))
            {
                this.ModelState.AddModelError(nameof(color.Name), this.takenColorNameErrorMessage);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(color);
            }

            await this.colorService.EditAsync(id, color.Name);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!await this.colorService.IsColorExistAsync(id))
            {
                return this.BadRequest(this.notExistingColorErrorMessage);
            }

            var color = await this.colorService.GetColorToDeleteByIdAsync(id);

            return this.View(color);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.colorService.DeleteColorAsync(id);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
