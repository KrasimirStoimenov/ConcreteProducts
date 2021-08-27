namespace ConcreteProducts.Test.Routing.Admin
{
    using NUnit.Framework;
    using MyTested.AspNetCore.Mvc;

    using ConcreteProducts.Web.Areas.Admin.Controllers;
    using ConcreteProducts.Web.Areas.Admin.Models.Colors;

    public class ColorsRouteTest
    {
        [Test]
        public void AllRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Colors/All?page=1")
                .To<ColorsController>(c => c.All(1));

        [Test]
        public void GetAddShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Colors/Add")
                .To<ColorsController>(c => c.Add());

        [Test]
        public void PostAddShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(response => response
                    .WithPath("/Admin/Colors/Add")
                    .WithMethod(HttpMethod.Post))
                .To<ColorsController>(c => c.Add(With.Any<ColorFormModel>()));

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void GetEditShouldBeMapped(int id)
            => MyRouting
                .Configuration()
                .ShouldMap($"/Admin/Colors/Edit/{id}")
                .To<ColorsController>(c => c.Edit(id));

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void PostEditShouldBeMapped(int id)
            => MyRouting
                .Configuration()
                .ShouldMap(response => response
                    .WithPath($"/Admin/Colors/Edit/{id}")
                    .WithMethod(HttpMethod.Post))
                .To<ColorsController>(c => c.Edit(With.Any<ColorFormModel>()));

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void GetDeleteShouldBeMapped(int id)
            => MyRouting
                .Configuration()
                .ShouldMap($"/Admin/Colors/Delete/{id}")
                .To<ColorsController>(c => c.Delete(id));

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void PostDeleteShouldBeMapped(int id)
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath($"/Admin/Colors/DeleteConfirmed/{id}")
                    .WithMethod(HttpMethod.Post))
                .To<ColorsController>(c => c.DeleteConfirmed(id));
    }
}