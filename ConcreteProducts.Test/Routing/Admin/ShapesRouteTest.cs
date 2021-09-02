namespace ConcreteProducts.Test.Routing.Admin
{
    using ConcreteProducts.Web.Areas.Admin.Controllers;
    using ConcreteProducts.Web.Areas.Admin.Models.Shapes;
    using MyTested.AspNetCore.Mvc;
    using NUnit.Framework;

    public class ShapesRouteTest
    {
        [Test]
        public void AllRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Shapes/All?page=1")
                .To<ShapesController>(c => c.All(1));

        [Test]
        public void GetAddShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Shapes/Add")
                .To<ShapesController>(c => c.Add());

        [Test]
        public void PostAddShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(response => response
                    .WithPath("/Admin/Shapes/Add")
                    .WithMethod(HttpMethod.Post))
                .To<ShapesController>(c => c.Add(With.Any<ShapeFormModel>()));

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void GetEditShouldBeMapped(int id)
            => MyRouting
                .Configuration()
                .ShouldMap($"/Admin/Shapes/Edit/{id}")
                .To<ShapesController>(c => c.Edit(id));

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void PostEditShouldBeMapped(int id)
            => MyRouting
                .Configuration()
                .ShouldMap(response => response
                    .WithPath($"/Admin/Shapes/Edit/{id}")
                    .WithMethod(HttpMethod.Post))
                .To<ShapesController>(c => c.Edit(id, With.Any<ShapeFormModel>()));

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void GetDeleteShouldBeMapped(int id)
            => MyRouting
                .Configuration()
                .ShouldMap($"/Admin/Shapes/Delete/{id}")
                .To<ShapesController>(c => c.Delete(id));

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void PostDeleteShouldBeMapped(int id)
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath($"/Admin/Shapes/DeleteConfirmed/{id}")
                    .WithMethod(HttpMethod.Post))
                .To<ShapesController>(c => c.DeleteConfirmed(id));
    }
}
