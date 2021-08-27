﻿namespace ConcreteProducts.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using static Common.GlobalConstants;

    [Authorize(Roles = AdministratorRoleName)]
    [Area(AdminAreaName)]
    public class AdminController : Controller
    {
    }
}
