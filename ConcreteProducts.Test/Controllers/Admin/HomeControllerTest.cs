namespace ConcreteProducts.Test.Controllers.Admin
{
    using ConcreteProducts.Web.Areas.Admin.Controllers;
    using MyTested.AspNetCore.Mvc;
    using NUnit.Framework;

    public class HomeControllerTest
    {
        [Test]
        public void IndexShouldReturnCorrectViewWithModel()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Index())
                .ShouldReturn()
                .Redirect("/WarehouseProducts/All");

        [Test]
        public void ErrorMessageShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .Calling(e => e.Error())
                .ShouldReturn()
                .View();
    }
}
