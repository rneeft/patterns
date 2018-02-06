using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chroomsoft.Queries.Tests
{
    [TestClass]
    public class QueryHandlerRegisterTest
    {
        private Guid guid;
        private QueryHandlerRegister queryHandler;

        [TestInitialize]
        public void TestInitialize()
        {
            guid = Guid.NewGuid();
            queryHandler = new QueryHandlerRegister();
        }

        [TestMethod]
        public void CanRegisterQueryHandler()
        {
            queryHandler.Register(new TestQueryHandler(guid));
        }

        [TestMethod]
        public void YouCanOnlyRegisterQueryOnce()
        {
            queryHandler.Register(new TestQueryHandler(guid));
            Action action = () => queryHandler.Register(new TestQueryHandler(guid));

            Assert.ThrowsException<HandlerAlreadyRegisteredException>(action);
        }

        [TestMethod]
        public void CanHandleTestQuery()
        {
            queryHandler.Register(new TestQueryHandler(guid));

            var expected = guid;
            var actual = queryHandler.Handle(new TestQuery());

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ThrowsExceptionUponNonRegistedQuery()
        {
            Action action = () => queryHandler.Handle(new TestQuery());

            Assert.ThrowsException<HandlerNotFoundException>(action);
        }
    }
}
