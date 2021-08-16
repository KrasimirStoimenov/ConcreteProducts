namespace ConcreteProducts.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class ChatController : Controller
    {
        public IActionResult Chat()
            => View();
    }
}
