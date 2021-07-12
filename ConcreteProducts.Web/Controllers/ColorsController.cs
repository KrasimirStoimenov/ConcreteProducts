namespace ConcreteProducts.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ConcreteProducts.Web.Data;
    using ConcreteProducts.Web.Models.Colors;
    using ConcreteProducts.Web.Data.Models;

    public class ColorsController : Controller
    {
        private readonly ConcreteProductsDbContext data;

        public ColorsController(ConcreteProductsDbContext data)
            => this.data = data;

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

            return RedirectToAction("Add", "Products");
        }
    }
}
