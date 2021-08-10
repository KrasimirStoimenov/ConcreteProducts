namespace ConcreteProducts.Test.Controllers
{
    using NUnit.Framework;
    using FluentAssertions;
    using MyTested.AspNetCore.Mvc;

    using ConcreteProducts.Web.Controllers;
    using ConcreteProducts.Web.Data.Models;
    using ConcreteProducts.Web.Models.WarehouseProducts;

    public class WarehouseProductsControllerTest
    {
        [Test]
        public void AllShouldReturnAllProductsInWarehouse()
            => MyController<WarehouseProductsController>
                .Instance()
                .Calling(c => c.All(1))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ListAllProductsInWarehouseViewModel>()
                    .Passing(model => model.PageNumber.Should().Be(1)));

        [Test]
        public void GetAddShouldBeForAuthorizedUsersAndReturnView()
            => MyController<WarehouseProductsController>
                .Instance()
                .Calling(c => c.Add())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AddProductToWarehouseFormModel>()
                        .Should()
                        .NotBeNull());

        [Test]
        public void PostAddShouldBeForAuthorizedUsersAndRedirectToActionWhenModelStateIsValid()
            => MyController<WarehouseProductsController>
                .Instance(controller => controller
                        .WithData(data => data
                        .WithEntities(new ProductColor())
                        .WithEntities(new Warehouse())))
                .Calling(c => c.Add(new AddProductToWarehouseFormModel
                {
                    Count = 1,
                    ProductColorId = 1,
                    WarehouseId = 1,
                }))
                .ShouldHave()
                .ActionAttributes(a => a
                    .RestrictingForAuthorizedRequests()
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");

        [Test]
        public void PostAddShouldBeForAuthorizedUsersAndReturnViewIfModelStateIsInvalid()
             => MyController<WarehouseProductsController>
                 .Instance(controller => controller
                         .WithData(data => data
                         .WithEntities(new ProductColor())
                         .WithEntities(new Warehouse())))
                 .Calling(c => c.Add(new AddProductToWarehouseFormModel
                 {
                     Count = 1,
                     ProductColorId = -1,
                     WarehouseId = -1,
                 }))
                 .ShouldHave()
                 .ActionAttributes(a => a
                     .RestrictingForAuthorizedRequests()
                     .RestrictingForHttpMethod(HttpMethod.Post))
                 .AndAlso()
                 .ShouldReturn()
                 .View();

        [Test]
        [TestCase("Something")]
        public void GetDecreaseQuantityShouldBeForAuthorizedUsersAndReturnViewIfExistingProductNameIsProvided(string productName)
            => MyController<WarehouseProductsController>
                .Instance()
                .Calling(c => c.DecreaseQuantity(productName))
                .ShouldHave()
                .ActionAttributes(a => a
                     .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<DecreaseQuantityViewModel>()
                    .Passing(data => data.ProductName.Should().BeSameAs(productName)));
    }
}
