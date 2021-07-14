namespace ConcreteProducts.Web.Controllers
{
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using ConcreteProducts.Web.Data;
    using ConcreteProducts.Web.Data.Models;
    using ConcreteProducts.Web.Models.Products;

    public class ProductsController : Controller
    {
        private readonly ConcreteProductsDbContext data;

        public ProductsController(ConcreteProductsDbContext data)
            => this.data = data;

        public IActionResult All()
        {
            var products = this.data.Products
                .Select(p => new ProductListingViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    QuantityInPalletInPieces = p.QuantityInPalletInPieces,
                    QuantityInPalletInUnitOfMeasurement = p.QuantityInPalletInUnitOfMeasurement,
                    UnitOfMeasurement = p.UnitOfMeasurement
                })
                .ToList();

            return View(products);
        }

        public IActionResult Add()
            => View(new AddProductFormModel
            {
                Categories = this.GetProductCategories(),
                Colors = this.GetProductColors()
            });

        [HttpPost]
        public IActionResult Add(AddProductFormModel product)
        {
            if (!this.data.Categories.Any(c => c.Id == product.CategoryId))
            {
                this.ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist.");
            }

            if (!this.data.Colors.Any(c => c.Id == product.ColorId))
            {
                this.ModelState.AddModelError(nameof(product.ColorId), "Color does not exist.");
            }

            if (!ModelState.IsValid)
            {
                product.Categories = this.GetProductCategories();
                product.Colors = this.GetProductColors();

                return View(product);
            }

            var currentProduct = new Product
            {
                Name = product.Name,
                QuantityInPalletInUnitOfMeasurement = product.QuantityInPalletInUnitOfMeasurement,
                QuantityInPalletInPieces = product.QuantityInPalletInPieces,
                CountInUnitOfMeasurement = product.CountInUnitOfMeasurement,
                UnitOfMeasurement = product.UnitOfMeasurement,
                Weight = product.Weight,
                CategoryId = product.CategoryId,
                ImageUrl = product.ImageUrl
            };

            currentProduct.ProductColors.Add(new ProductColor
            {
                ColorId = product.ColorId
            });

            this.data.Products.Add(currentProduct);
            this.data.SaveChanges();
            var categoryProducts = this.data.Categories.SelectMany(c => c.Products);

            return RedirectToAction("Index", "Home");
        }

        private IEnumerable<ProductCategoryViewModel> GetProductCategories()
            => this.data
                .Categories
                .Select(c => new ProductCategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

        private IEnumerable<ProductColorViewModel> GetProductColors()
            => this.data
                .Colors
                .Select(c => new ProductColorViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();
    }
}
