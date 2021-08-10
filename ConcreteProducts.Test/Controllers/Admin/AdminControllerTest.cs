namespace ConcreteProducts.Test.Controllers.Admin
{
    using NUnit.Framework;
    using MyTested.AspNetCore.Mvc;

    using ConcreteProducts.Web.Areas.Admin;

    using AdminController = Web.Areas.Admin.Controllers.AdminController;

    public class AdminControllerTest
    {
        [Test]
        public void ControllerShouldBeInAdminArea()
            => MyController<AdminController>
                .ShouldHave()
                .Attributes(attribute => attribute
                    .SpecifyingArea(AdminConstants.AreaName)
                    .RestrictingForAuthorizedRequests(AdminConstants.AdministratorRoleName));
    }
}
