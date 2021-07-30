﻿namespace ConcreteProducts.Web.Infrastructure
{
    using System.Security.Claims;

    using static ConcreteProducts.Web.Areas.Admin.AdminConstants;

    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;

        public static bool IsAdmin(this ClaimsPrincipal user)
            => user.IsInRole(AdministratorRoleName);
    }
}