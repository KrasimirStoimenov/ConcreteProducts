namespace ConcreteProducts.Test.Controllers
{
    using NUnit.Framework;
    using FluentAssertions;
    using MyTested.AspNetCore.Mvc;

    using ConcreteProducts.Web.Controllers;
    using ConcreteProducts.Data.Models;
    using ConcreteProducts.Web.Models.Products;
    using ConcreteProducts.Data.Models.Enumerations;
    using ConcreteProducts.Services.Products.Models;

    using static Common.GlobalConstants;

    public class ProductsControllerTest
    {
        [Test]
        public void AllShouldReturnAllProducts()
            => MyController<ProductsController>
                .Instance()
                .Calling(c => c.All(null, 1))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ListAllProductsViewModel>()
                    .Passing(model =>
                    {
                        model.SearchTerm.Should().BeNull();
                        model.PageNumber.Should().Be(1);
                        model.HasNextPage.Should().Be(false);
                        model.HasPreviousPage.Should().Be(false);
                        model.PagesCount.Should().Be(0);
                        model.PageNumber.Should().Be(1);
                        model.NextPageNumber.Should().Be(2);
                        model.PreviousPageNumber.Should().Be(0);
                    }));
        [Test]
        public void GetAddShouldReturnView()
            => MyController<ProductsController>
                .Instance()
                .Calling(c => c.Add())
                .ShouldHave()
                .ActionAttributes(attribute => attribute
                    .RestrictingForAuthorizedRequests(AdministratorRoleName))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AddProductFormModel>()
                    .Passing(model =>
                    {
                        model.Categories.Should().BeNullOrEmpty();
                        model.Colors.Should().BeNullOrEmpty();
                    }));

        [Test]
        [TestCase("Test")]
        [TestCase("Something")]
        public void PostAddShouldRedirectToActionIfModelStateIsValid(string name)
            => MyController<ProductsController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Color { Name = name })
                    .WithEntities(new Category { Name = name }))
                .Calling(c => c.Add(new AddProductFormModel
                {
                    Name = name,
                    CategoryId = 1,
                    ColorId = 1,
                    CountInUnitOfMeasurement = 15,
                    Dimensions = "Dimensions",
                    QuantityInPalletInPieces = 15,
                    QuantityInPalletInUnitOfMeasurement = 15,
                    Weight = 15,
                    UnitOfMeasurement = UnitOfMeasurement.Meters

                }))
                .ShouldHave()
                .ActionAttributes(attribute => attribute
                    .RestrictingForAuthorizedRequests(AdministratorRoleName)
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");

        [Test]
        public void PostAddShouldReturnViewIfModelStateIsInvalid()
            => MyController<ProductsController>
                .Instance()
                .Calling(c => c.Add(With.Default<AddProductFormModel>()))
                .ShouldHave()
                .ActionAttributes(attribute => attribute
                    .RestrictingForAuthorizedRequests(AdministratorRoleName)
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AddProductFormModel>()
                    .Passing(model =>
                    {
                        model.Categories.Should().BeNullOrEmpty();
                        model.Colors.Should().BeEmpty();
                    }));

        [Test]
        public void PostAddShouldReturnViewIfPassingSameNameAndDimensions()
            => MyController<ProductsController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Category { Name = "Test" })
                    .WithEntities(new Color { Name = "Test" })
                    .WithEntities(new Product { Name = "Test", Dimensions = "Test" }))
                .Calling(c => c.Add(new AddProductFormModel
                {
                    Name = "Test",
                    Dimensions = "Test",
                    CategoryId = 1,
                    ColorId = 1
                }))
                .ShouldHave()
                .ActionAttributes(attribute => attribute
                    .RestrictingForAuthorizedRequests(AdministratorRoleName)
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AddProductFormModel>()
                    .Passing(model =>
                    {
                        model.Name.Should().BeSameAs("Test");
                        model.Dimensions.Should().BeSameAs("Test");
                        model.CategoryId.Should().Be(1);
                        model.ColorId.Should().Be(1);
                        model.Colors.Should().NotBeNullOrEmpty();
                        model.Categories.Should().NotBeNullOrEmpty();
                    }));

        [Test]
        [TestCase(1)]
        [TestCase(5)]
        public void GetDetailsShouldReturnViewIfIdExist(int id)
            => MyController<ProductsController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Product
                    {
                        Id = id,
                        Name = "test",
                        CategoryId = 1,
                        CountInUnitOfMeasurement = 15,
                        Dimensions = "Dimensions",
                        QuantityInPalletInPieces = 15,
                        QuantityInPalletInUnitOfMeasurement = 15,
                        Weight = 15,
                        ImageUrl = "https://chilli.codes/wp-content/uploads/2020/10/git.jpg",
                        UnitOfMeasurement = UnitOfMeasurement.Meters,
                        Category = new Category { Id = 1, Name = "Test" }
                    }))
                .Calling(c => c.Details(id))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ProductDetailsServiceModel>()
                    .Passing(model =>
                    {
                        model.Id.Should().Be(id);
                        model.Name.Should().BeSameAs("test");
                        model.CategoryName.Should().BeSameAs("Test");
                        model.CountInUnitOfMeasurement.Should().Be(15);
                        model.Dimensions.Should().BeSameAs("Dimensions");
                        model.Weight.Should().Be(15);
                        model.QuantityInPalletInPieces.Should().Be(15);
                        model.QuantityInPalletInUnitOfMeasurement.Should().Be(15);
                    }));


        [Test]
        [TestCase(5)]
        [TestCase(10)]
        public void GetDetailsShouldReturnBadRequestIfIdDoesNotExist(int id)
            => MyController<ProductsController>
                .Instance()
                .Calling(c => c.Details(id))
                .ShouldReturn()
                .BadRequest();

        [Test]
        [TestCase(1)]
        [TestCase(5)]
        public void GetDeleteShouldBeForAdminsOnlyAndShouldReturnViewIfIdExists(int id)
            => MyController<ProductsController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Product { Id = id }))
                .Calling(c => c.Delete(id))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(AdministratorRoleName))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ProductBaseServiceModel>()
                    .Passing(model => model.Id.Should().Be(id)));

        [Test]
        [TestCase(1)]
        [TestCase(5)]
        public void GetDeleteShouldReturnBadRequestIfNoSuchIdExist(int id)
            => MyController<ProductsController>
                .Instance()
                .Calling(c => c.Delete(id))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests(AdministratorRoleName))
                .AndAlso()
                .ShouldReturn()
                .BadRequest();

        [Test]
        [TestCase(1)]
        public void PostDeleteShouldBeForAdminsOnlyAndShouldRedirectToActionIfModelStateIsValid(int id)
            => MyController<ProductsController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Product { Id = id }))
                .Calling(c => c.DeleteConfirmed(id))
                .ShouldHave()
                .ActionAttributes(attribute => attribute
                    .RestrictingForAuthorizedRequests(AdministratorRoleName)
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");
    }
}
