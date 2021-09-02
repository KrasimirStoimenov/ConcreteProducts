namespace ConcreteProducts.Test.Controllers.Admin
{
    using Microsoft.AspNetCore.Identity;
    using MyTested.AspNetCore.Mvc;
    using NUnit.Framework;

    using UserController = ConcreteProducts.Web.Areas.Admin.Controllers.UsersController;

    public class UsersControllerTest
    {
        [Test]
        public void PromoteShouldWorkAsExpected()
            => MyController<UserController>
                .Instance()
                .WithUser()
                .WithData(GetRole("Employee"), new IdentityUser { Id = "test1", UserName = "test", Email = "test@test.com" })
                .Calling(c => c.Promote("test1"))
                .ShouldReturn()
                .RedirectToAction("All");

        [Test]
        public void DemoteShouldWorkAsExpected()
            => MyController<UserController>
                .Instance()
                .WithUser()
                .WithData(GetRole("Basic"), new IdentityUser { Id = "test1", UserName = "test", Email = "test@test.com" })
                .Calling(c => c.Demote("test1"))
                .ShouldReturn()
                .RedirectToAction("All");

        private static IdentityRole GetRole(string name)
        {
            return new IdentityRole
            {
                Name = name,
                NormalizedName = name.ToUpper(),
            };
        }
    }
}
