namespace ConcreteProducts.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using static AdminConstants;

    [Authorize(Roles = AdministratorRoleName)]
    [Area(AreaName)]
    public class AdminController : Controller
    {
    }
}
