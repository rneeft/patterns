    using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chroomsoft.Commands.Test
{
    [TestClass]
    public class CommandRegisterTests
    {
        private ICommandHandlerRegister registar;

        [TestInitialize]
        public void TestInitialize()
        {
            registar = new CommandRegister();
        }

        [TestMethod]
        public void CanRegister()
        {
            registar.Register(new TestCommandHandler());
        }

        [TestMethod]
        public void YouCanOnlyCommandHandlerOnce()
        {
            registar.Register(new TestCommandHandler());
            Action action = () => registar.Register(new TestCommandHandler());

            Assert.ThrowsException<CommandHandlerAlreadyRegisteredException>(action);
        }

        [TestMethod]
        public void CanHandleTestCommand()
        {
            var handler = new TestCommandHandler();

            registar.Register(handler);

            registar.Handle(new TestCommand());

            Assert.IsTrue(handler.CommandHandlerCalled);
        }

        [TestMethod]
        public void ThrowsExceptionUponNonRegistedQuery()
        {
            Action action = () => registar.Handle(new TestCommand());

            Assert.ThrowsException<CommandHandlerNotFoundException>(action);
        }
    }
}
