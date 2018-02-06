namespace Chroomsoft.Commands
{
    public interface ICommandHandlerRegister
    {
        void Handle<TCommand>(TCommand command);
        void Register<TCommand>(ICommandHandler<TCommand> handler) where TCommand : ICommand;
    }
}
