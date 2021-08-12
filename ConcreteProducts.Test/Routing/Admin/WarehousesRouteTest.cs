namespace ConcreteProducts.Test.Routing.Admin
{
    using NUnit.Framework;
    using MyTested.AspNetCore.Mvc;

    using ConcreteProducts.Web.Areas.Admin.Controllers;
using ConcreteProducts.Web.Areas.Admin.Models.Warehouses;

    public class WarehousesRouteTest
    {
        [Test]
        public void AllRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Warehouses/All?page=1")
                .To<WarehousesController>(c => c.All(1));

        [Test]
        public void GetAddShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Warehouses/Add")
                .To<WarehousesController>(c => c.Add());

        [Test]
        public void PostAddShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(response => response
                    .WithPath("/Admin/Warehouses/Add")
                    .WithMethod(HttpMethod.Post))
                .To<WarehousesController>(c => c.Add(With.Any<WarehouseFormModel>()));

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void GetEditShouldBeMapped(int id)
            => MyRouting
                .Configuration()
                .ShouldMap($"/Admin/Warehouses/Edit/{id}")
                .To<WarehousesController>(c => c.Edit(id));

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void PostEditShouldBeMapped(int id)
            => MyRouting
                .Configuration()
                .ShouldMap(response => response
                    .WithPath($"/Admin/Warehouses/Edit/{id}")
                    .WithMethod(HttpMethod.Post))
                .To<WarehousesController>(c => c.Edit(id, With.Any<WarehouseFormModel>()));

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void GetDeleteShouldBeMapped(int id)
            => MyRouting
                .Configuration()
                .ShouldMap($"/Admin/Warehouses/Delete/{id}")
                .To<WarehousesController>(c => c.Delete(id));

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void PostDeleteShouldBeMapped(int id)
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath($"/Admin/Warehouses/DeleteConfirmed/{id}")
                    .WithMethod(HttpMethod.Post))
                .To<WarehousesController>(c => c.DeleteConfirmed(id));
    }
}