namespace ConcreteProducts.Test.Routing.Admin
{
    using NUnit.Framework;
    using MyTested.AspNetCore.Mvc;

    using ConcreteProducts.Web.Areas.Admin.Controllers;

    public class UsersRouteTest
    {
        [Test]
        public void AllUsersRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Users/All")
                .To<UsersController>(c => c.All());

        [Test]
        public void PromoteRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Users/Promote/test1")
                .To<UsersController>(c => c.Promote("test1"));

        [Test]
        public void DemoteRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Users/Demote/test1")
                .To<UsersController>(c => c.Demote("test1"));
    }
}
