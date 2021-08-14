namespace ConcreteProducts.Test.Routing
{
    using NUnit.Framework;
    using MyTested.AspNetCore.Mvc;

    using ConcreteProducts.Web.Controllers;
    using ConcreteProducts.Web.Models.Products;

    public class ProductRouteTest
    {
        [Test]
        public void AllRouteWithoutSearchTermShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Products/All?page=1")
                .To<ProductsController>(c => c.All(null, 1));

        [Test]
        [TestCase("Test", 1)]
        [TestCase("Something", 2)]
        public void AllRouteWithSearchTermShouldBeMapped(string searchTerm, int page)
            => MyRouting
                .Configuration()
                .ShouldMap($"/Products/All?SearchTerm={searchTerm}&page={page}")
                .To<ProductsController>(c => c.All(searchTerm, page));

        [Test]
        public void GetAddRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Products/Add")
                .To<ProductsController>(c => c.Add());

        [Test]
        public void PostAddRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(map => map
                    .WithPath("/Products/Add")
                    .WithMethod(HttpMethod.Post))
                .To<ProductsController>(c => c.Add(With.Any<AddProductFormModel>()));

        [Test]
        [TestCase(1)]
        [TestCase(5)]
        public void GetDetailsRouteShouldBeMapped(int id)
            => MyRouting
                .Configuration()
                .ShouldMap($"/Products/Details/{id}")
                .To<ProductsController>(c => c.Details(id));

        [Test]
        [TestCase(1)]
        [TestCase(5)]
        public void GetDeleteRouteShouldBeMapped(int id)
            => MyRouting
                .Configuration()
                .ShouldMap($"/Products/Delete/{id}")
                .To<ProductsController>(c => c.Delete(id));

        [Test]
        [TestCase(1)]
        [TestCase(5)]
        public void PostDeleteRouteShouldBeMapped(int id)
            => MyRouting
                .Configuration()
                .ShouldMap(map => map
                    .WithPath($"/Products/DeleteConfirmed/{id}")
                    .WithMethod(HttpMethod.Post))
                .To<ProductsController>(c => c.DeleteConfirmed(id));

    }
}
