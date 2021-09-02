namespace ConcreteProducts.Test.Controllers
{
    using ConcreteProducts.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using NUnit.Framework;

    public class ChatControllerTest
    {
        [Test]
        public void ChatShouldHaveAuthorizeAttribute()
            => MyController<ChatController>
                .Instance()
                .ShouldHave()
                .Attributes(c => c.RestrictingForAuthorizedRequests());

        [Test]
        public void ChatShouldReturnView()
            => MyController<ChatController>
                .Instance()
                .Calling(c => c.Chat())
                .ShouldReturn()
                .View();
    }
}
