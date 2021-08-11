namespace ConcreteProducts.Test.Controllers.Admin
{
    using NUnit.Framework;
    using FluentAssertions;
    using MyTested.AspNetCore.Mvc;

    using ConcreteProducts.Web.Data.Models;
    using ConcreteProducts.Web.Services.Shapes.Models;
    using ConcreteProducts.Web.Areas.Admin.Models.Shapes;

    using ShapeController = Web.Areas.Admin.Controllers.ShapesController;

    public class ShapesControllerTest
    {
        [Test]
        public void AllShouldReturnAllShapes()
           => MyController<ShapeController>
               .Instance()
               .Calling(c => c.All(1))
               .ShouldReturn()
               .View(view => view.WithModelOfType<ListAllShapesViewModel>()
                   .Should()
                   .NotBeNull());

        [Test]
        public void GetAddShouldReturnView()
            => MyController<ShapeController>
                .Instance()
                .Calling(c => c.Add())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ShapeFormModel>()
                        .Should()
                        .NotBeNull());

        [Test]
        public void PostAddShouldRedirectToActionWhenModelStateIsValid()
            => MyController<ShapeController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Warehouse() { Id = 1 }))
                .Calling(c => c.Add(new ShapeFormModel
                {
                    Name = "Test",
                    Dimensions = "Test",
                    WarehouseId = 1
                }))
                .ShouldHave()
                .ActionAttributes(attribute => attribute
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");

        [Test]
        public void PostAddShouldReturnViewWhenModelStateIsInvalid()
            => MyController<ShapeController>
                .Instance()
                .Calling(c => c.Add(With.Default<ShapeFormModel>()))
                .ShouldHave()
                .ActionAttributes(attribute => attribute
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ShapeFormModel>().Should().NotBeNull());

        [Test]
        public void PostAddShouldReturnViewWhenExistingShapeNameIsPassed()
            => MyController<ShapeController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Shape { Name = "Exist" }))
                .Calling(c => c.Add(new ShapeFormModel { Name = "Exist" }))
                .ShouldHave()
                .ActionAttributes(attribute => attribute
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ShapeFormModel>().Should().NotBeNull());

        [Test]
        [TestCase(1, "Test")]
        [TestCase(1, "AnotherTest")]
        public void GetEditShouldReturnViewIfValidIdIsPassed(int id, string name)
            => MyController<ShapeController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Shape { Id = id, Name = name }))
                .Calling(c => c.Edit(1))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ShapeFormModel>()
                    .Passing(model =>
                    {
                        model.Name.Should().BeSameAs(name);
                    }));

        [Test]
        public void GetEditShouldReturnBadRequestIfInvalidIdIsPassed()
            => MyController<ShapeController>
                .Instance()
                .Calling(c => c.Edit(2))
                .ShouldReturn()
                .BadRequest();

        [Test]
        public void PostEditShouldReturnRedirectToActionIfModelStateIsValid()
            => MyController<ShapeController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Shape { Id = 1, Name = "Test" }))
                .Calling(c => c.Edit(1, new ShapeFormModel
                {
                    Name = "Something",
                    Dimensions = "Test"
                }))
                .ShouldHave()
                .ActionAttributes(attribute => attribute
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");

        [Test]
        public void PostEditShouldReturnViewIfHasShapeWithSameName()
            => MyController<ShapeController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Shape { Id = 1, Name = "Test" }))
                .Calling(c => c.Edit(1, new ShapeFormModel
                {
                    Name = "Test"
                }))
                .ShouldHave()
                .ActionAttributes(attribute => attribute
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ShapeFormModel>().Should().NotBeNull());

        [Test]
        public void PostEditShouldReturnViewIfInvalidIdIsPassed()
            => MyController<ShapeController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Shape { Id = 1, Name = "Test" }))
                .Calling(c => c.Edit(2, With.Default<ShapeFormModel>()))
                .ShouldHave()
                .ActionAttributes(attribute => attribute
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ShapeFormModel>().Should().NotBeNull());

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void GetDeleteShouldReturnViewIfModelStateIsValid(int id)
            => MyController<ShapeController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Shape { Id = id }))
                .Calling(c => c.Delete(id))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ShapeBaseServiceModel>()
                    .Passing(model => model.Id.Should().BeGreaterOrEqualTo(id)));

        [Test]
        [TestCase(5)]
        [TestCase(-1)]
        public void GetDeleteShouldReturnBadRequestIfInvalidIdIsPassed(int id)
            => MyController<ShapeController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Shape { Id = 1 }))
                .Calling(c => c.Delete(id))
                .ShouldReturn()
                .BadRequest();

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void PostDeleteConfirmShouldRedirectToActionIfModelStateIsValid(int id)
            => MyController<ShapeController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Shape { Id = id }))
                .Calling(c => c.DeleteConfirmed(id))
                .ShouldHave()
                .ActionAttributes(attribute => attribute
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");
    }
}
