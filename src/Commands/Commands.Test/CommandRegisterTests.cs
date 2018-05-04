using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chroomsoft.Commands.Test
{
    [TestClass]
    public class CommandRegisterTests
    {
        private ICommandHandlerRegister registar;

        [TestMethod]
        public void CanRegister()
        {
            registar.Register(new TestCommandAsyncHandler());
        }

        [TestMethod]
        public async Task CanExecuteAsyncTestCommand()
        {
            var handler = new TestCommandAsyncHandler();

            registar.Register(handler);

            await registar.ExecuteAsync(new TestCommand());

            Assert.IsTrue(handler.CommandHandlerCalled);
        }

        [TestMethod]
        public void CanRegisterSyncCommandHandler()
        {
            registar.Register(new TestCommandHandler());
        }

        [TestMethod]
        public async Task ExceptionThrownInASyncCommandHandlerAreFlowingThroughTheExecuteAsyncMethod()
        {
            registar.Register(new ExceptionCommandHandler());

            Func<Task> action = () => registar.ExecuteAsync(new ExceptionTestCommand());

            await Assert.ThrowsExceptionAsync<TestException>(action);
        }

        [TestMethod]
        public async Task ExceptionThrownInSyncCommandHandlerAreFlowingThroughTheExecuteAsyncMethod()
        {
            registar.Register(new ExceptionSyncCommandHandler());

            Func<Task> action = () => registar.ExecuteAsync(new ExceptionTestCommand());

            await Assert.ThrowsExceptionAsync<TestException>(action);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            registar = new CommandRegister();
        }

        [TestMethod]
        public async Task ThrowsExceptionUponNonRegistedQuery()
        {
            Func<Task> action = () => registar.ExecuteAsync(new TestCommand());

            await Assert.ThrowsExceptionAsync<CommandHandlerNotFoundException>(action);
        }

        [TestMethod]
        public void YouCanOnlyAsyncCommandHandlerOnce()
        {
            registar.Register(new TestCommandAsyncHandler());

            Action action = () => registar.Register(new TestCommandAsyncHandler());

            Assert.ThrowsException<CommandHandlerAlreadyRegisteredException>(action);
        }

        [TestMethod]
        public void YouCanRegisterSyncCommandHandlerOnlyOnce()
        {
            registar.Register(new TestCommandHandler());

            Action action = () => registar.Register(new TestCommandHandler());

            Assert.ThrowsException<CommandHandlerAlreadyRegisteredException>(action);
        }
    }
}