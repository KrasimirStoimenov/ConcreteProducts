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

        public IActionResult All(int id = 1)
        {
            const int itemsPerPage = 8;

            var productsToList = this.data.Products
                .OrderByDescending(p => p.Id)
                .Skip((id - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(p => new ProductInListViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Dimensions = p.Dimensions,
                    InPallet = $"{p.QuantityInPalletInPieces} pieces / {p.QuantityInPalletInUnitOfMeasurement}                          {p.UnitOfMeasurement}",
                    DefaultImageUrl = p.ProductColors
                                        .Where(c => c.Color.Name == "Grey")
                                        .Select(pc => pc.ImageUrl)
                                        .FirstOrDefault(),
                })
                .ToList();

            var productsViewModel = new ProductListingViewModel
            {
                ItemsPerPage = itemsPerPage,
                Products = productsToList,
                ProductsCount = productsToList.Count,
                PageNumber = id
            };

            return View(productsViewModel);
        }

        public IActionResult Add()
            => View(new AddProductFormModel
            {
                Categories = this.GetProductCategories(),
                Colors = this.GetProductColors(),
                Warehouses = this.GetProductWarehouses()
            });

        [HttpPost]
        public IActionResult Add(AddProductFormModel product)
        {
            this.ValidateCollections(product);

            if (!ModelState.IsValid)
            {
                product.Categories = this.GetProductCategories();
                product.Colors = this.GetProductColors();
                product.Warehouses = this.GetProductWarehouses();

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

        private IEnumerable<ProductWarehouseViewModel> GetProductWarehouses()
            => this.data
                .Warehouses
                .Select(w => new ProductWarehouseViewModel
                {
                    Id = w.Id,
                    Name = w.Name
                })
                .ToList();
    }
}
