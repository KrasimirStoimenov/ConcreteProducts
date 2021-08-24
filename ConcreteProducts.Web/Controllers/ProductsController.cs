﻿namespace ConcreteProducts.Web.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using ConcreteProducts.Data;
    using ConcreteProducts.Web.Models.Products;
    using ConcreteProducts.Services.Products;
    using ConcreteProducts.Services.Colors;
    using ConcreteProducts.Services.Categories;
    using ConcreteProducts.Services.Warehouses;

    using static GlobalConstants;

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

        public IActionResult All(string searchTerm, int page = 1)
        {
            if (searchTerm != null)
            {
                page = 1;
            }

            var products = this.productService.GetAllListingProducts(searchTerm);

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
        public IActionResult Add()
            => View(new AddProductFormModel
            {
                Categories = this.categoryService.GetAllCategories(),
                Colors = this.colorService.GetAllColors(),
            });

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public IActionResult Add(AddProductFormModel product)
        {
            this.ValidateCollections(product);

            if (!ModelState.IsValid)
            {
                product.Categories = this.categoryService.GetAllCategories();
                product.Colors = this.colorService.GetAllColors();

                return View(product);
            }

            this.productService.Create(
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

        public IActionResult Details(int id)
        {
            if (!this.productService.IsProductExist(id))
            {
                return BadRequest(notExistingProduct);
            }

            var productDetails = this.productService.GetProductDetails(id);

            return View(productDetails);
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Delete(int id)
        {
            if (!this.productService.IsProductExist(id))
            {
                return BadRequest(notExistingProduct);
            }

            var product = this.productService.GetProductToDeleteById(id);

            return View(product);
        }

        [Authorize(Roles = AdministratorRoleName)]
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

            if (this.productService.HasProductWithSameNameAndDimensions(product.Name, product.Dimensions))
            {
                this.ModelState.AddModelError(nameof(product.Name), existProductWithSameParameters);
            }
        }
    }
}
