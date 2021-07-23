namespace ConcreteProducts.Web.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using ConcreteProducts.Web.Data;
    using ConcreteProducts.Web.Data.Models;
    using ConcreteProducts.Web.Models.Products;
    using ConcreteProducts.Web.Services.Products;
    using ConcreteProducts.Web.Services.Colors;
    using ConcreteProducts.Web.Services.Categories;
    using ConcreteProducts.Web.Services.Warehouses;

    public class ProductsController : Controller
    {
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

        public IActionResult Add()
            => View(new AddProductFormModel
            {
                Categories = this.categoryService.GetAllCategories(),
                Colors = this.colorService.GetAllColors(),
                Warehouses = this.warehouseService.GetAllWarehouses()
            });

        [HttpPost]
        public IActionResult Add(AddProductFormModel product)
        {
            this.ValidateCollections(product);

            if (!ModelState.IsValid)
            {
                product.Categories = this.categoryService.GetAllCategories();
                product.Colors = this.colorService.GetAllColors();
                product.Warehouses = this.warehouseService.GetAllWarehouses();

                return View(product);
            }

            var currentProduct = this.data.Products
                .Select(p => new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Dimensions = p.Dimensions,
                    ProductColors = p.ProductColors
                })
                .FirstOrDefault(p => p.Name == product.Name && p.Dimensions == product.Dimensions);

            if (currentProduct == null)
            {
                currentProduct = new Product
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
                    ColorId = product.ColorId,
                    ImageUrl = product.ImageUrl
                });

                this.data.Products.Add(currentProduct);
            }

            if (!currentProduct.ProductColors.Any(pc => pc.ColorId == product.ColorId))
            {
                this.data.ProductColors.Add(new ProductColor
                {
                    ProductId = currentProduct.Id,
                    ColorId = product.ColorId,
                    ImageUrl = product.ImageUrl
                });

            }

            this.data.SaveChanges();

            return RedirectToAction("All");
        }

        public IActionResult Details(int id)
        {
            if (!this.productService.IsProductExist(id))
            {
                return BadRequest("Product does not exist.");
            }

            var productDetails = this.productService.GetProductDetails(id);

            return View(productDetails);
        }

        public IActionResult Delete(int id)
        {
            var validateProductExist = this.productService.IsProductExist(id);
            if (!validateProductExist)
            {
                return RedirectToAction(nameof(All));
            }

            this.productService.DeleteProduct(id);

            return RedirectToAction(nameof(All));
        }

        private void ValidateCollections(AddProductFormModel product)
        {
            if (!this.data.Categories.Any(c => c.Id == product.CategoryId))
            {
                this.ModelState.AddModelError(nameof(product.CategoryId), $"{nameof(product.CategoryId)} does not exist.");
            }

            if (!this.data.Colors.Any(c => c.Id == product.ColorId))
            {
                this.ModelState.AddModelError(nameof(product.ColorId), $"{nameof(product.ColorId)} does not exist.");
            }

            if (!this.data.Warehouses.Any(c => c.Id == product.WarehouseId))
            {
                this.ModelState.AddModelError(nameof(product.WarehouseId), $"{nameof(product.WarehouseId)} does not exist.");
            }
        }
    }
}
