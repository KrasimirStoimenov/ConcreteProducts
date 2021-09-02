namespace ConcreteProducts.Test.Controllers.Admin
{
    using MyTested.AspNetCore.Mvc;
    using NUnit.Framework;

    using static Common.GlobalConstants;

    using AdminController = ConcreteProducts.Web.Areas.Admin.Controllers.AdminController;

    public class AdminControllerTest
    {
        [Test]
        public void ControllerShouldBeInAdminArea()
            => MyController<AdminController>
                .ShouldHave()
                .Attributes(attribute => attribute
                    .SpecifyingArea(AdminAreaName)
                    .RestrictingForAuthorizedRequests(AdministratorRoleName));
    }
}
