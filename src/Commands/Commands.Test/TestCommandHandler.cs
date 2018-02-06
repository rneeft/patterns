namespace Chroomsoft.Commands.Test
{
    public class TestCommandHandler : ICommandHandler<TestCommand>
    {
        public void Handle(TestCommand command)
        {
            CommandHandlerCalled = true;
        }

        public bool CommandHandlerCalled { get; set; }
    }
}
