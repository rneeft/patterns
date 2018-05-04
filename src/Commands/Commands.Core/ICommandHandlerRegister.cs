using System.Threading.Tasks;

namespace Chroomsoft.Commands
{
    public interface ICommandHandlerRegister
    {
        Task ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand;

        void Register<TCommand>(IAsyncCommandHandler<TCommand> handler) where TCommand : ICommand;
    }
}