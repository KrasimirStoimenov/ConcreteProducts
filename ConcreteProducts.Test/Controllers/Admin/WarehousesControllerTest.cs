﻿namespace ConcreteProducts.Test.Controllers.Admin
{
    using NUnit.Framework;
    using FluentAssertions;
    using MyTested.AspNetCore.Mvc;

    using ConcreteProducts.Web.Data.Models;
    using ConcreteProducts.Web.Services.Warehouses.Models;
    using ConcreteProducts.Web.Areas.Admin.Models.Warehouses;

    using WarehouseController = Web.Areas.Admin.Controllers.WarehousesController;

    public class WarehousesControllerTest
    {
        [Test]
        public void AllShouldReturnAllWarehouses()
           => MyController<WarehouseController>
               .Instance()
               .Calling(c => c.All(1))
               .ShouldReturn()
               .View(view => view.WithModelOfType<ListAllWarehouseViewModel>()
                   .Should()
                   .NotBeNull());

        [Test]
        public void GetAddShouldReturnView()
            => MyController<WarehouseController>
                .Instance()
                .Calling(c => c.Add())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<WarehouseFormModel>()
                        .Should()
                        .NotBeNull());

        [Test]
        public void PostAddShouldRedirectToActionWhenModelStateIsValid()
            => MyController<WarehouseController>
                .Instance()
                .Calling(c => c.Add(new WarehouseFormModel
                {
                    Name = "Test"
                }))
                .ShouldHave()
                .ActionAttributes(attribute => attribute
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");

        [Test]
        public void PostAddShouldReturnViewWhenModelStateIsInvalid()
            => MyController<WarehouseController>
                .Instance()
                .Calling(c => c.Add(With.Default<WarehouseFormModel>()))
                .ShouldHave()
                .ActionAttributes(attribute => attribute
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<WarehouseFormModel>()
                        .Should()
                        .NotBeNull());

        [Test]
        public void PostAddShouldReturnViewWhenExistingWarehouseNameIsPassed()
                    => MyController<WarehouseController>
                        .Instance()
                        .WithData(data => data
                            .WithEntities(new Warehouse { Name = "Exist" }))
                        .Calling(c => c.Add(new WarehouseFormModel { Name = "Exist" }))
                        .ShouldHave()
                        .ActionAttributes(attribute => attribute
                            .RestrictingForHttpMethod(HttpMethod.Post))
                        .AndAlso()
                        .ShouldReturn()
                        .View(view => view
                            .WithModelOfType<WarehouseFormModel>().Should().NotBeNull());

        [Test]
        [TestCase(1, "Test")]
        [TestCase(1, "AnotherTest")]
        public void GetEditShouldReturnViewIfValidIdIsPassed(int id, string name)
            => MyController<WarehouseController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Warehouse { Id = id, Name = name }))
                .Calling(c => c.Edit(1))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<WarehouseFormModel>()
                    .Passing(model =>
                    {
                        model.Name.Should().BeSameAs(name);
                    }));
        [Test]
        public void GetEditShouldReturnBadRequestIfInvalidIdIsPassed()
                    => MyController<WarehouseController>
                        .Instance()
                        .Calling(c => c.Edit(2))
                        .ShouldReturn()
                        .BadRequest();

        [Test]
        public void PostEditShouldReturnRedirectToActionIfModelStateIsValid()
            => MyController<WarehouseController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Warehouse { Id = 1, Name = "Test" }))
                .Calling(c => c.Edit(1, new WarehouseFormModel
                {
                    Name = "Something"
                }))
                .ShouldHave()
                .ActionAttributes(attribute => attribute
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");
        [Test]
        public void PostEditShouldReturnViewIfHasWarehouseWithSameName()
                    => MyController<WarehouseController>
                        .Instance()
                        .WithData(data => data
                            .WithEntities(new Warehouse { Id = 1, Name = "Test" }))
                        .Calling(c => c.Edit(1, new WarehouseFormModel
                        {
                            Name = "Test"
                        }))
                        .ShouldHave()
                        .ActionAttributes(attribute => attribute
                            .RestrictingForHttpMethod(HttpMethod.Post))
                        .AndAlso()
                        .ShouldReturn()
                        .View(view => view
                            .WithModelOfType<WarehouseFormModel>().Should().NotBeNull());

        [Test]
        public void PostEditShouldReturnViewIfInvalidIdIsPassed()
            => MyController<WarehouseController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Warehouse { Id = 1, Name = "Test" }))
                .Calling(c => c.Edit(2, With.Default<WarehouseFormModel>()))
                .ShouldHave()
                .ActionAttributes(attribute => attribute
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<WarehouseFormModel>().Should().NotBeNull());

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void GetDeleteShouldReturnViewIfModelStateIsValid(int id)
            => MyController<WarehouseController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Warehouse { Id = id }))
                .Calling(c => c.Delete(id))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<WarehouseWithProductsAndShapesCount>()
                    .Passing(model => model.Id.Should().BeGreaterOrEqualTo(id)));

        [Test]
        [TestCase(5)]
        [TestCase(-1)]
        public void GetDeleteShouldReturnBadRequestIfInvalidIdIsPassed(int id)
            => MyController<WarehouseController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Warehouse { Id = 1 }))
                .Calling(c => c.Delete(id))
                .ShouldReturn()
                .BadRequest();

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void PostDeleteConfirmShouldRedirectToActionIfModelStateIsValid(int id)
            => MyController<WarehouseController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Warehouse { Id = id }))
                .Calling(c => c.DeleteConfirmed(id))
                .ShouldHave()
                .ActionAttributes(attribute => attribute
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");
    }
}