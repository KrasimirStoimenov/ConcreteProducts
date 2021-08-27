namespace ConcreteProducts.Web.Infrastructure.Extensions
{
    using System.Security.Claims;

    using static Common.GlobalConstants;

    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;

        public static bool IsAdmin(this ClaimsPrincipal user)
            => user.IsInRole(AdministratorRoleName);

        public static bool IsWorker(this ClaimsPrincipal user)
            => user.IsInRole(EmployeeRoleName);
    }
}
