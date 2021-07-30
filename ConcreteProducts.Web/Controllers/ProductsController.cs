namespace ConcreteProducts.Web.Controllers
{
    using System.Linq;
    using System.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using ConcreteProducts.Web.Data;
    using ConcreteProducts.Web.Data.Models;
    using ConcreteProducts.Web.Models.Products;
    using ConcreteProducts.Web.Services.Products;
    using ConcreteProducts.Web.Services.Colors;
    using ConcreteProducts.Web.Services.Categories;
    using ConcreteProducts.Web.Services.Warehouses;
    using ConcreteProducts.Web.Areas.Admin;

    public class ProductsController : Controller
    {
        private const string notExistingProduct = "Product does not exist.";
        private const string existProductWithSameParameters = "Product already exist.";

        private readonly IProductService productService;
        private readonly IColorService colorService;
        private readonly ICategoryService categoryService;
        private readonly IWarehouseService warehouseService;
        private readonly ConcreteProductsDbContext data;

        public ProductsController(IProductService productService, ConcreteProductsDbContext data, IColorService colorService, ICategoryService categoryService, IWarehouseService warehouseService)
        {
            this.productService = productService;
            this.data = data;
            this.colorService = colorService;
            this.categoryService = categoryService;
            this.warehouseService = warehouseService;
        }

        public IActionResult All(string searchTerm, int id = 1)
        {
            const int itemsPerPage = 8;

            if (searchTerm != null)
            {
                id = 1;
            }

            var products = this.productService.GetAllProducts(searchTerm);

            var productsViewModel = new ListAllProductsViewModel
            {
                AllProducts = products.Skip((id - 1) * itemsPerPage).Take(itemsPerPage),
                PageNumber = id,
                Count = products.Count(),
                ItemsPerPage = itemsPerPage
            };
            return View(productsViewModel);
        }

        [Authorize(Roles = AdminConstants.AdministratorRoleName)]
        public IActionResult Add()
            => View(new AddProductFormModel
            {
                Categories = this.categoryService.GetAllCategories(),
                Colors = this.colorService.GetAllColors(),
                Warehouses = this.warehouseService.GetAllWarehouses()
            });

        [Authorize(Roles = AdminConstants.AdministratorRoleName)]
        [HttpPost]
        public IActionResult Add(AddProductFormModel product)
        {
            this.ValidateCollections(product);

            if (this.productService.HasProductWithSameNameAndDimensions(product.Name, product.Dimensions))
            {
                this.ModelState.AddModelError(nameof(product.Name), existProductWithSameParameters);
            }

            if (!ModelState.IsValid)
            {
                product.Categories = this.categoryService.GetAllCategories();
                product.Colors = this.colorService.GetAllColors();
                product.Warehouses = this.warehouseService.GetAllWarehouses();

                return View(product);
            }

            var currentProduct = new Product
            {
                Name = product.Name,
                Dimensions = product.Dimensions,
                QuantityInPalletInUnitOfMeasurement = product.QuantityInPalletInUnitOfMeasurement,
                QuantityInPalletInPieces = product.QuantityInPalletInPieces,
                CountInUnitOfMeasurement = product.CountInUnitOfMeasurement,
                UnitOfMeasurement = product.UnitOfMeasurement,
                Weight = product.Weight,
                CategoryId = product.CategoryId,
                WarehouseId = product.WarehouseId
            };

            currentProduct.ProductColors.Add(new ProductColor
            {
                ColorId = product.ColorId
            });


            this.data.Products.Add(currentProduct);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        public IActionResult Details(int id)
        {
            if (!this.productService.IsProductExist(id))
            {
                return BadRequest(notExistingProduct);
            }

            var productDetails = this.productService.GetProductDetails(id);

            return View(productDetails);
        }

        [Authorize(Roles = AdminConstants.AdministratorRoleName)]
        public IActionResult Delete(int id)
        {
            if (!this.productService.IsProductExist(id))
            {
                return BadRequest(notExistingProduct);
            }

            var product = this.productService.GetProductToDeleteById(id);

            return View(product);
        }

        [Authorize(Roles = AdminConstants.AdministratorRoleName)]
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            this.productService.DeleteProduct(id);

            return RedirectToAction(nameof(All));
        }

        private void ValidateCollections(AddProductFormModel product)
        {
            if (!this.categoryService.IsCategoryExist(product.CategoryId))
            {
                this.ModelState.AddModelError(nameof(product.CategoryId), $"Category does not exist.");
            }

            if (!this.colorService.IsColorExist(product.ColorId))
            {
                this.ModelState.AddModelError(nameof(product.ColorId), $"Color does not exist.");
            }

            if (!this.warehouseService.IsWarehouseExist(product.WarehouseId))
            {
                this.ModelState.AddModelError(nameof(product.WarehouseId), $"Warehouse does not exist.");
            }
        }
    }
}
