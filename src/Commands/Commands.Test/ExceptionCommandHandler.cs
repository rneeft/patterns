using System.Threading.Tasks;

namespace Chroomsoft.Commands.Test
{
    public class ExceptionCommandHandler : IAsyncCommandHandler<ExceptionTestCommand>
    {
        public async Task HandleAsync(ExceptionTestCommand command)
        {
            await Task.Delay(0);

            throw new TestException();
        }
    }
}