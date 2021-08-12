namespace ConcreteProducts.Test.Controllers.Admin
{
    using NUnit.Framework;
    using FluentAssertions;
    using MyTested.AspNetCore.Mvc;

    using ConcreteProducts.Web.Areas.Admin.Models.ProductColors;

    using ProductColorsController = Web.Areas.Admin.Controllers.ProductColorsController;
    using ConcreteProducts.Web.Data.Models;

    public class ProductColorsControllerTest
    {
        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void GetAddShouldReturnView(int productId)
            => MyController<ProductColorsController>
                .Instance()
                .Calling(c => c.Add(productId))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AddColorToProductFormModel>()
                    .Passing(model => model.ProductId.Should().Be(productId)));

        [Test]
        public void PostAddShouldReturnRedirect()
            => MyController<ProductColorsController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Product())
                    .WithEntities(new Color()))
                .Calling(c => c.Add(new AddColorToProductFormModel
                {
                    ProductId = 1,
                    ColorId = 1,
                }))
                .ShouldReturn()
                .Redirect();

        [Test]
        public void PostAddShouldReturnViewIfProductAndColorDoesNotExist()
            => MyController<ProductColorsController>
                .Instance()
                .Calling(c => c.Add(With.Default<AddColorToProductFormModel>()))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AddColorToProductFormModel>());

        [Test]
        public void PostAddShouldReturnViewIfProductHasThisColor()
            => MyController<ProductColorsController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Color())
                    .WithEntities(new Product())
                    .WithEntities(new ProductColor { ColorId = 1, ProductId = 1 }))
                .Calling(c => c.Add(new AddColorToProductFormModel
                {
                    ColorId = 1,
                    ProductId = 1
                }))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AddColorToProductFormModel>()
                    .Passing(model =>
                    {
                        model.ColorId.Should().Be(1);
                        model.ProductId.Should().Be(1);
                    }));
    }
}
