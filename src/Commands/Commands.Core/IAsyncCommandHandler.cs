using System.Threading.Tasks;

namespace Chroomsoft.Commands
{
    public interface IAsyncCommandHandler<TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command);
    }
}