namespace ConcreteProducts.Web.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using ConcreteProducts.Services.Categories;
    using ConcreteProducts.Services.Colors;
    using ConcreteProducts.Services.Products;
    using ConcreteProducts.Services.Warehouses;
    using ConcreteProducts.Web.Models.Products;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using static Common.GlobalConstants;

    public class ProductsController : Controller
    {
        private const string notExistingProduct = "Product does not exist.";
        private const string existProductWithSameParameters = "Product already exist.";

        private readonly IProductService productService;
        private readonly IColorService colorService;
        private readonly ICategoryService categoryService;
        private readonly Cloudinary cloudinary;

        public ProductsController(IProductService productService, IColorService colorService, ICategoryService categoryService, IWarehouseService warehouseService, Cloudinary cloudinary)
        {
            this.productService = productService;
            this.colorService = colorService;
            this.categoryService = categoryService;
            this.cloudinary = cloudinary;
        }

        public async Task<IActionResult> All(string searchTerm, int page = 1)
        {
            if (searchTerm != null)
            {
                page = 1;
            }

            var products = await this.productService
                    .GetAllListingProductsAsync(searchTerm);

            var productsViewModel = new ListAllProductsViewModel
            {
                AllProducts = products
                    .Skip((page - 1) * ItemsPerPage)
                    .Take(ItemsPerPage),
                PageNumber = page,
                Count = products.Count(),
                ItemsPerPage = ItemsPerPage,
            };
            return this.View(productsViewModel);
        }

        [Authorize(Roles = AdministratorRoleName)]
        public async Task<IActionResult> Add()
            => this.View(new AddProductFormModel
            {
                Categories = await this.categoryService.GetAllCategoriesAsync(),
                Colors = await this.colorService.GetAllColorsAsync(),
            });

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> Add(AddProductFormModel product)
        {
            var hasValidNameAndDimensions = await this.productService
                    .HasProductWithSameNameAndDimensionsAsync(product.Name, product.Dimensions);

            if (hasValidNameAndDimensions)
            {
                this.ModelState.AddModelError(nameof(product.Name), existProductWithSameParameters);
            }

            if (!this.ModelState.IsValid)
            {
                product.Categories = await this.categoryService.GetAllCategoriesAsync();
                product.Colors = await this.colorService.GetAllColorsAsync();

                return this.View(product);
            }

            var cloudinaryUrl = await this.UploadFileToCloudinary(product.Image);

            await this.productService.CreateAsync(
                product.Name,
                product.Dimensions,
                product.QuantityInPalletInUnitOfMeasurement,
                product.QuantityInPalletInPieces,
                product.CountInUnitOfMeasurement,
                product.UnitOfMeasurement,
                product.Weight,
                cloudinaryUrl,
                product.CategoryId,
                product.ColorId);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Details(int id)
        {
            var productExist = await this.productService.IsProductExistAsync(id);

            if (!productExist)
            {
                return this.BadRequest(notExistingProduct);
            }

            var productDetails = await this.productService.GetProductDetailsAsync(id);

            return this.View(productDetails);
        }

        [Authorize(Roles = AdministratorRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            var productExist = await this.productService.IsProductExistAsync(id);

            if (!productExist)
            {
                return this.BadRequest(notExistingProduct);
            }

            var product = await this.productService.GetProductToDeleteByIdAsync(id);

            return this.View(product);
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.productService.DeleteProductAsync(id);

            return this.RedirectToAction(nameof(this.All));
        }

        private async Task<string> UploadFileToCloudinary(IFormFile image)
        {
            using var memoryStream = new MemoryStream();

            await image.CopyToAsync(memoryStream);

            using var newStream = new MemoryStream(memoryStream.ToArray());

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(image.FileName, newStream),
            };

            var uploadResult = this.cloudinary.Upload(uploadParams);

            return uploadResult.Url.AbsoluteUri;
        }
    }
}
