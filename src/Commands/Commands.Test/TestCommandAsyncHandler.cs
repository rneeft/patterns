using System.Threading.Tasks;

namespace Chroomsoft.Commands.Test
{
    public class TestCommandAsyncHandler : IAsyncCommandHandler<TestCommand>
    {
        public bool CommandHandlerCalled { get; set; }

        public async Task HandleAsync(TestCommand command)
        {
            await Task.Delay(1);
            CommandHandlerCalled = true;
        }
    }
}