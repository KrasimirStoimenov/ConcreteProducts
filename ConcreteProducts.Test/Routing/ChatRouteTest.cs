namespace ConcreteProducts.Test.Routing
{
    using ConcreteProducts.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using NUnit.Framework;

    public class ChatRouteTest
    {
        [Test]
        public void ChatRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Chat/Chat")
                .To<ChatController>(c => c.Chat());
    }
}
