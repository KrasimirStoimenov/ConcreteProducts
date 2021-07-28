namespace ConcreteProducts.Web.Infrastructure
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Routing;

    public static class EndpointBuilderExtensions
    {
        public static void MapAreaControllerRoute(this IEndpointRouteBuilder endpoints)
            => endpoints.MapControllerRoute(
            name: "Areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    }
}
