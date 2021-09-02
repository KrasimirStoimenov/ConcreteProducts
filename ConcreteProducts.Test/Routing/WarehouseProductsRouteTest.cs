namespace ConcreteProducts.Test.Routing
{
    using ConcreteProducts.Web.Controllers;
    using ConcreteProducts.Web.Models.WarehouseProducts;
    using MyTested.AspNetCore.Mvc;
    using NUnit.Framework;

    public class WarehouseProductsRouteTest
    {
        [Test]
        public void AllRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/WarehouseProducts/All?page=1")
                .To<WarehouseProductsController>(c => c.All(1));

        [Test]
        public void GetAddRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/WarehouseProducts/Add")
                .To<WarehouseProductsController>(c => c.Add());

        [Test]
        public void PostAddRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/WarehouseProducts/Add")
                    .WithMethod(HttpMethod.Post))
                .To<WarehouseProductsController>(c => c.Add(With.Any<AddProductToWarehouseFormModel>()));
    }
}
