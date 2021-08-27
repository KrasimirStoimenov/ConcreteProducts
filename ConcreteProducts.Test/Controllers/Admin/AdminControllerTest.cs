namespace ConcreteProducts.Test.Controllers.Admin
{
    using NUnit.Framework;
    using MyTested.AspNetCore.Mvc;

    using static Common.GlobalConstants;

    using AdminController = Web.Areas.Admin.Controllers.AdminController;

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
