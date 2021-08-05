namespace ConcreteProducts.Web.Controllers
{
    using ConcreteProducts.Web.Models.WarehouseProducts;
    using ConcreteProducts.Web.Services.Products;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class WarehouseProductsController : Controller
    {
        private readonly IProductService productService;

        public WarehouseProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [Authorize]
        public IActionResult All()
        {
            return View();
        }

        [Authorize]
        public IActionResult Add(int warehouseId)
            => View(new AddProductToWarehouseFormModel
            {
                WarehouseId = warehouseId,
                Products = this.productService.GetAllProducts()
            });

        [Authorize]
        [HttpPost]
        public IActionResult Add(AddProductToWarehouseFormModel model)
        {
            if (!this.productService.IsProductExist(model.ProductId))
            {
                this.ModelState.AddModelError(nameof(model.ProductId), $"Product does not exist.");
            }

            if (!ModelState.IsValid)
            {
                model.Products = this.productService.GetAllProducts();

                return View(model);
            }



            return RedirectToAction(nameof(All));
        }
    }
}
