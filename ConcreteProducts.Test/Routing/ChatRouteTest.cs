namespace ConcreteProducts.Test.Routing
{
    using NUnit.Framework;
    using MyTested.AspNetCore.Mvc;

    using ConcreteProducts.Web.Controllers;

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
