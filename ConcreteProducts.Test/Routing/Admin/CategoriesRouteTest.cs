namespace ConcreteProducts.Test.Routing.Admin
{
    using ConcreteProducts.Web.Areas.Admin.Controllers;
    using ConcreteProducts.Web.Areas.Admin.Models.Categories;
    using MyTested.AspNetCore.Mvc;
    using NUnit.Framework;

    public class CategoriesRouteTest
    {
        [Test]
        public void AllRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Categories/All?page=1")
                .To<CategoriesController>(c => c.All(1));

        [Test]
        public void GetAddShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Categories/Add")
                .To<CategoriesController>(c => c.Add());

        [Test]
        public void PostAddShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(response => response
                    .WithPath("/Admin/Categories/Add")
                    .WithMethod(HttpMethod.Post))
                .To<CategoriesController>(c => c.Add(With.Any<CategoryFormModel>()));

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void GetEditShouldBeMapped(int id)
            => MyRouting
                .Configuration()
                .ShouldMap($"/Admin/Categories/Edit/{id}")
                .To<CategoriesController>(c => c.Edit(id));

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void PostEditShouldBeMapped(int id)
            => MyRouting
                .Configuration()
                .ShouldMap(response => response
                    .WithPath($"/Admin/Categories/Edit/{id}")
                    .WithMethod(HttpMethod.Post))
                .To<CategoriesController>(c => c.Edit(id, With.Any<CategoryFormModel>()));

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void GetDeleteShouldBeMapped(int id)
            => MyRouting
                .Configuration()
                .ShouldMap($"/Admin/Categories/Delete/{id}")
                .To<CategoriesController>(c => c.Delete(id));

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void PostDeleteShouldBeMapped(int id)
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath($"/Admin/Categories/DeleteConfirmed/{id}")
                    .WithMethod(HttpMethod.Post))
                .To<CategoriesController>(c => c.DeleteConfirmed(id));
    }
}
