namespace ConcreteProducts.Test.Controllers
{
    using NUnit.Framework;
    using MyTested.AspNetCore.Mvc;

    using ConcreteProducts.Web.Controllers;

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
