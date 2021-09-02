namespace ConcreteProducts.Web.Infrastructure.Extensions
{
    using ConcreteProducts.Web.Hubs;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Routing;

    public static class EndpointBuilderExtensions
    {
        public static void MapAreaControllerRoute(this IEndpointRouteBuilder endpoints)
            => endpoints.MapControllerRoute(
            name: "Areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        public static void MapChatHubRoute(this IEndpointRouteBuilder endpoints)
            => endpoints.MapHub<ChatHub>("/Chat");
    }
}
