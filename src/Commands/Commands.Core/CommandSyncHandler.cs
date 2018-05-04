using System.Threading.Tasks;

namespace Chroomsoft.Commands
{
    public abstract class CommandSyncHandler<TCommand> : IAsyncCommandHandler<TCommand> where TCommand : ICommand
    {
        public virtual Task HandleAsync(TCommand command)
        {
            Handle(command);

            return Task.FromResult(0);
        }

        protected abstract void Handle(TCommand command);
    }
}