using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chroomsoft.Queries.Tests
{
    [TestClass]
    public class QueryHandlerRegisterTest
    {
        private Guid guid;
        private QueryHandlerRegister register;

        [TestInitialize]
        public void TestInitialize()
        {
            guid = Guid.NewGuid();
            register = new QueryHandlerRegister();
        }

        [TestMethod]
        public void CanRegisterQueryHandler()
        {
            register.Register(new TestQueryHandler(guid));
        }

        [TestMethod]
        public void YouCanOnlyRegisterQueryOnce()
        {
            register.Register(new TestQueryHandler(guid));
            Action action = () => register.Register(new TestQueryHandler(guid));

            Assert.ThrowsException<HandlerAlreadyRegisteredException>(action);
        }

        [TestMethod]
        public void CanHandleTestQuery()
        {
            register.Register(new TestQueryHandler(guid));

            var expected = guid;
            var actual = register.Handle(new TestQuery());

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ThrowsExceptionUponNonRegistedQuery()
        {
            Action action = () => register.Handle(new TestQuery());

            Assert.ThrowsException<HandlerNotFoundException>(action);
        }
    }
}
