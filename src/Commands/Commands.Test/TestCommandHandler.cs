namespace Chroomsoft.Commands.Test
{
    public class TestCommandHandler : CommandSyncHandler<TestCommand>
    {
        public bool CommandHandlerCalled { get; set; }

        protected override void Handle(TestCommand command)
        {
            CommandHandlerCalled = true;
        }
    }
}