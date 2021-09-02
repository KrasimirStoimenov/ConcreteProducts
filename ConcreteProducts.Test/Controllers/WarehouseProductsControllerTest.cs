namespace ConcreteProducts.Test.Controllers
{
    using ConcreteProducts.Data.Models;
    using ConcreteProducts.Web.Controllers;
    using ConcreteProducts.Web.Models.WarehouseProducts;
    using FluentAssertions;
    using MyTested.AspNetCore.Mvc;
    using NUnit.Framework;

    using static Common.GlobalConstants;

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
        public void GetDecreaseQuantityShouldBeForAdminsAndReturnView(string productName)
            => MyController<WarehouseProductsController>
                .Instance()
                .Calling(c => c.DecreaseQuantity(productName))
                .ShouldHave()
                .ActionAttributes(a => a
                     .RestrictingForAuthorizedRequests(AdministratorRoleName))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<DecreaseQuantityViewModel>()
                    .Passing(data => data.ProductName.Should().BeSameAs(productName)));

        [Test]
        public void PostDecreaseQuantityShouldBeForAdminsOnlyAndShouldReturnRedirectToActionIfModelStateIsValid()
            => MyController<WarehouseProductsController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new WarehouseProductColors
                    {
                        ProductColor = new ProductColor(),
                        ProductColorId = 1,
                        Warehouse = new Warehouse(),
                        WarehouseId = 1,
                        Count = 15,
                    }))
                .Calling(c => c.DecreaseQuantity(new DecreaseQuantityViewModel
                {
                    ProductColorId = 1,
                    WarehouseId = 1,
                    Count = 10,
                }))
                .ShouldHave()
                .ActionAttributes(a => a
                     .RestrictingForAuthorizedRequests(AdministratorRoleName))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");

        [Test]
        public void PostDecreaseQuantityShouldBeForAdminsOnlyAndShouldReturnViewIfModelStateIsInvalid()
            => MyController<WarehouseProductsController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new WarehouseProductColors { ProductColor = new ProductColor(), ProductColorId = 1, Warehouse = new Warehouse(), WarehouseId = 1, Count = 15 }))
                .Calling(c => c.DecreaseQuantity(new DecreaseQuantityViewModel
                {
                    ProductColorId = 1,
                    WarehouseId = 1,
                    Count = 20,
                }))
                .ShouldHave()
                .ActionAttributes(a => a
                     .RestrictingForAuthorizedRequests(AdministratorRoleName))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<DecreaseQuantityViewModel>()
                    .Passing(model =>
                    {
                        model.Count.Should().Be(20);
                        model.WarehouseId.Should().Be(1);
                        model.ProductColorId.Should().Be(1);
                    }));

        [Test]
        public void PostDecreaseQuantityShouldBeForAdminsOnlyAndShouldReturnViewIfDoesntHaveQuantityInStock()
            => MyController<WarehouseProductsController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Warehouse())
                    .WithEntities(new ProductColor())
                    .WithEntities(new WarehouseProductColors { ProductColorId = 1, WarehouseId = 1, Count = 15 }))
                .Calling(c => c.DecreaseQuantity(new DecreaseQuantityViewModel
                {
                    ProductColorId = 1,
                    WarehouseId = 1,
                    Count = 20,
                }))
                .ShouldHave()
                .ActionAttributes(a => a
                     .RestrictingForAuthorizedRequests(AdministratorRoleName))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<DecreaseQuantityViewModel>()
                    .Passing(model =>
                    {
                        model.Count.Should().Be(20);
                        model.ProductColorId.Should().Be(1);
                        model.WarehouseId.Should().Be(1);
                    }));
    }
}
