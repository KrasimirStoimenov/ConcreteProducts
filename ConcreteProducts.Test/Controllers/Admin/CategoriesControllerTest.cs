namespace ConcreteProducts.Test.Controllers.Admin
{
    using ConcreteProducts.Data.Models;
    using ConcreteProducts.Services.Categories.Models;
    using ConcreteProducts.Web.Areas.Admin.Models.Categories;
    using FluentAssertions;
    using MyTested.AspNetCore.Mvc;
    using NUnit.Framework;

    using CategoriesController = ConcreteProducts.Web.Areas.Admin.Controllers.CategoriesController;

    public class CategoriesControllerTest
    {
        [Test]
        public void AllShouldReturnAllCategories()
            => MyController<CategoriesController>
                .Instance()
                .Calling(c => c.All(1))
                .ShouldReturn()
                .View(view => view.WithModelOfType<ListAllCategoriesViewModel>()
                    .Should()
                    .NotBeNull());

        [Test]
        public void GetAddShouldReturnView()
            => MyController<CategoriesController>
                .Instance()
                .Calling(c => c.Add())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<CategoryFormModel>()
                        .Should()
                        .NotBeNull());

        [Test]
        public void PostAddShouldRedirectToActionWhenModelStateIsValid()
            => MyController<CategoriesController>
                .Instance()
                .Calling(c => c.Add(new CategoryFormModel
                {
                    Name = "Test",
                }))
                .ShouldHave()
                .ActionAttributes(attribute => attribute
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");

        [Test]
        public void PostAddShouldReturnViewWhenModelStateIsInvalid()
            => MyController<CategoriesController>
                .Instance()
                .Calling(c => c.Add(With.Default<CategoryFormModel>()))
                .ShouldHave()
                .ActionAttributes(attribute => attribute
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<CategoryFormModel>().Should().NotBeNull());

        [Test]
        public void PostAddShouldReturnViewWhenExistingCategoryNameIsPassed()
            => MyController<CategoriesController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Category { Name = "Exist" }))
                .Calling(c => c.Add(new CategoryFormModel { Name = "Exist" }))
                .ShouldHave()
                .ActionAttributes(attribute => attribute
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<CategoryFormModel>().Should().NotBeNull());

        [Test]
        [TestCase(1, "Test")]
        [TestCase(1, "AnotherTest")]
        public void GetEditShouldReturnViewIfValidIdIsPassed(int id, string name)
            => MyController<CategoriesController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Category { Id = id, Name = name }))
                .Calling(c => c.Edit(1))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<CategoryFormModel>()
                    .Passing(model =>
                    {
                        model.Name.Should().BeSameAs(name);
                    }));

        [Test]
        public void GetEditShouldReturnBadRequestIfInvalidIdIsPassed()
            => MyController<CategoriesController>
                .Instance()
                .Calling(c => c.Edit(2))
                .ShouldReturn()
                .BadRequest();

        [Test]
        public void PostEditShouldReturnRedirectToActionIfModelStateIsValid()
            => MyController<CategoriesController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Category { Id = 1, Name = "Test" }))
                .Calling(c => c.Edit(1, new CategoryFormModel
                {
                    Name = "Something",
                }))
                .ShouldHave()
                .ActionAttributes(attribute => attribute
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");

        [Test]
        public void PostEditShouldReturnViewIfHasCategoryWithSameName()
            => MyController<CategoriesController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Category { Id = 1, Name = "Test", Products = null }))
                .Calling(c => c.Edit(1, new CategoryFormModel
                {
                    Name = "Test",
                }))
                .ShouldHave()
                .ActionAttributes(attribute => attribute
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<CategoryFormModel>()
                    .Passing(model =>
                    {
                        model.Name.Should().BeSameAs("Test");
                        model.Should().NotBeNull();
                    }));

        [Test]
        public void PostEditShouldReturnViewIfInvalidIdIsPassed()
            => MyController<CategoriesController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Category { Id = 1, Name = "Test" }))
                .Calling(c => c.Edit(1, With.Default<CategoryFormModel>()))
                .ShouldHave()
                .ActionAttributes(attribute => attribute
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<CategoryFormModel>().Should().NotBeNull());

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void GetDeleteShouldReturnViewIfModelStateIsValid(int id)
            => MyController<CategoriesController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Category { Id = id, Products = null }))
                .Calling(c => c.Delete(id))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<CategoryWithProducts>()
                    .Passing(model =>
                    {
                        model.Id.Should().BeGreaterOrEqualTo(id);
                        model.ProductsCount.Should().Be(0);
                    }));

        [Test]
        [TestCase(5)]
        [TestCase(-1)]
        public void GetDeleteShouldReturnBadRequestIfInvalidIdIsPassed(int id)
            => MyController<CategoriesController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Category { Id = 1 }))
                .Calling(c => c.Delete(id))
                .ShouldReturn()
                .BadRequest();

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void PostDeleteConfirmShouldRedirectToActionIfModelStateIsValid(int id)
            => MyController<CategoriesController>
                .Instance()
                .WithData(data => data
                    .WithEntities(new Category { Id = id }))
                .Calling(c => c.DeleteConfirmed(id))
                .ShouldHave()
                .ActionAttributes(attribute => attribute
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");
    }
}
