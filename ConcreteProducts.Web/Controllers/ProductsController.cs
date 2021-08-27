namespace ConcreteProducts.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using ConcreteProducts.Data;
    using ConcreteProducts.Web.Models.Products;
    using ConcreteProducts.Services.Products;
    using ConcreteProducts.Services.Colors;
    using ConcreteProducts.Services.Categories;
    using ConcreteProducts.Services.Warehouses;

    using static Common.GlobalConstants;

    public class ProductsController : Controller
    {
        private const string notExistingProduct = "Product does not exist.";
        private const string existProductWithSameParameters = "Product already exist.";

        private readonly IProductService productService;
        private readonly IColorService colorService;
        private readonly ICategoryService categoryService;

        public ProductsController(IProductService productService, ConcreteProductsDbContext data, IColorService colorService, ICategoryService categoryService, IWarehouseService warehouseService)
        {
            this.productService = productService;
            this.colorService = colorService;
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> All(string searchTerm, int page = 1)
        {
            if (searchTerm != null)
            {
                page = 1;
            }

            var products = await this.productService.GetAllListingProductsAsync(searchTerm);

            var productsViewModel = new ListAllProductsViewModel
            {
                AllProducts = products
                    .Skip((page - 1) * ItemsPerPage)
                    .Take(ItemsPerPage),
                PageNumber = page,
                Count = products.Count(),
                ItemsPerPage = ItemsPerPage
            };
            return View(productsViewModel);
        }

        [Authorize(Roles = AdministratorRoleName)]
        public async Task<IActionResult> Add()
            => View(new AddProductFormModel
            {
                Categories = await this.categoryService.GetAllCategoriesAsync(),
                Colors = await this.colorService.GetAllColorsAsync(),
            });

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> Add(AddProductFormModel product)
        {
            await this.ValidateCollections(product);

            if (!ModelState.IsValid)
            {
                product.Categories = await this.categoryService.GetAllCategoriesAsync();
                product.Colors = await this.colorService.GetAllColorsAsync();

                return View(product);
            }

            await this.productService.CreateAsync(
                product.Name,
                product.Dimensions,
                product.QuantityInPalletInUnitOfMeasurement,
                product.QuantityInPalletInPieces,
                product.CountInUnitOfMeasurement,
                product.UnitOfMeasurement,
                product.Weight,
                product.ImageUrl,
                product.CategoryId,
                product.ColorId);

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Details(int id)
        {
            if (!await this.productService.IsProductExistAsync(id))
            {
                return BadRequest(notExistingProduct);
            }

            var productDetails = await this.productService.GetProductDetailsAsync(id);

            return View(productDetails);
        }

        [Authorize(Roles = AdministratorRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await this.productService.IsProductExistAsync(id))
            {
                return BadRequest(notExistingProduct);
            }

            var product = await this.productService.GetProductToDeleteByIdAsync(id);

            return View(product);
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.productService.DeleteProductAsync(id);

            return RedirectToAction(nameof(All));
        }

        private async Task ValidateCollections(AddProductFormModel product)
        {
            if (!await this.categoryService.IsCategoryExistAsync(product.CategoryId))
            {
                this.ModelState.AddModelError(nameof(product.CategoryId), $"Category does not exist.");
            }

            if (!await this.colorService.IsColorExistAsync(product.ColorId))
            {
                this.ModelState.AddModelError(nameof(product.ColorId), $"Color does not exist.");
            }

            if (await this.productService.HasProductWithSameNameAndDimensionsAsync(product.Name, product.Dimensions))
            {
                this.ModelState.AddModelError(nameof(product.Name), existProductWithSameParameters);
            }
        }
    }
}
