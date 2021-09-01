namespace ConcreteProducts.Test.Controllers.Admin
{
    using NUnit.Framework;
    using FluentAssertions;
    using MyTested.AspNetCore.Mvc;

    using ConcreteProducts.Data.Models;
    using ConcreteProducts.Services.Colors.Models;
    using ConcreteProducts.Web.Areas.Admin.Models.Colors;

    using ColorController = Web.Areas.Admin.Controllers.ColorsController;

    public class ColorsControllerTest
    {
        [Test]
        public void AllShouldReturnAllColors()
            => MyController<ColorController>
                .Instance()
                .Calling(c => c.All(1))
                .ShouldReturn()
                .View(view => view.WithModelOfType<ListAllColorsViewModel>()
                    .Should()
                    .NotBeNull());

        [Test]
        public void GetAddShouldReturnView()
            => MyController<ColorController>
                .Instance()
                .Calling(c => c.Add())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ColorFormModel>()
                        .Should()
                        .NotBeNull());

        [Test]
        public void PostAddShouldRedirectToActionWhenModelStateIsValid()
            => MyController<ColorController>
                .Instance()
                .Calling(c => c.Add(new ColorFormModel
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
            => MyController<ColorController>
                .Instance()
                .Calling(c => c.Add(With.Default<ColorFormModel>()))
                .ShouldHave()
                .ActionAttributes(attribute => attribute
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ColorFormModel>().Should().NotBeNull());

        [Test]
        public void PostAddShouldReturnViewWhenExistingColorNameIsPassed()
            => MyController<ColorController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Color { Name = "Exist" }))
                .Calling(c => c.Add(new ColorFormModel { Name = "Exist" }))
                .ShouldHave()
                .ActionAttributes(attribute => attribute
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ColorFormModel>().Should().NotBeNull());

        [Test]
        [TestCase(1, "Test")]
        [TestCase(1, "AnotherTest")]
        public void GetEditShouldReturnViewIfValidIdIsPassed(int id, string name)
            => MyController<ColorController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Color { Id = id, Name = name }))
                .Calling(c => c.Edit(1))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ColorFormModel>()
                    .Passing(model =>
                    {
                        model.Name.Should().BeSameAs(name);
                    }));

        [Test]
        public void GetEditShouldReturnBadRequestIfInvalidIdIsPassed()
            => MyController<ColorController>
                .Instance()
                .Calling(c => c.Edit(2))
                .ShouldReturn()
                .BadRequest();

        [Test]
        public void PostEditShouldReturnRedirectToActionIfModelStateIsValid()
            => MyController<ColorController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Color { Id = 1, Name = "Test" }))
                .Calling(c => c.Edit(1,new ColorFormModel
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
        public void PostEditShouldReturnViewIfHasColorWithSameName()
            => MyController<ColorController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Color { Id = 1, Name = "Test", ProductColors = null }))
                .Calling(c => c.Edit(1,new ColorFormModel
                {
                    Name = "Test"
                }))
                .ShouldHave()
                .ActionAttributes(attribute => attribute
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ColorFormModel>()
                    .Passing(model => model.Name.Should().BeSameAs("Test")));

        [Test]
        public void PostEditShouldReturnViewIfInvalidIdIsPassed()
            => MyController<ColorController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Color { Id = 1, Name = "Test" }))
                .Calling(c => c.Edit(1,With.Default<ColorFormModel>()))
                .ShouldHave()
                .ActionAttributes(attribute => attribute
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ColorFormModel>().Should().NotBeNull());

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void GetDeleteShouldReturnViewIfModelStateIsValid(int id)
            => MyController<ColorController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Color { Id = id })
                    .WithEntities())
                .Calling(c => c.Delete(id))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ColorDeleteServiceModel>()
                    .Passing(model =>
                    {
                        model.Id.Should().BeGreaterOrEqualTo(id);
                        model.ProductsRelatedToColor.Should().Be(0);
                    }));

        [Test]
        [TestCase(5)]
        [TestCase(-1)]
        public void GetDeleteShouldReturnBadRequestIfInvalidIdIsPassed(int id)
            => MyController<ColorController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Color { Id = 1 }))
                .Calling(c => c.Delete(id))
                .ShouldReturn()
                .BadRequest();

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void PostDeleteConfirmShouldRedirectToActionIfModelStateIsValid(int id)
            => MyController<ColorController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Color { Id = id }))
                .Calling(c => c.DeleteConfirmed(id))
                .ShouldHave()
                .ActionAttributes(attribute => attribute
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");
    }
}
