using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chroomsoft.Commands
{
    public class CommandRegister : ICommandHandlerRegister
    {
        private readonly Dictionary<Type, object> handlers = new Dictionary<Type, object>();

        public async Task ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            var commandType = command.GetType();

            if (!handlers.ContainsKey(commandType))
                throw new CommandHandlerNotFoundException(commandType);

            var handlerAsync = (Func<TCommand, Task>)this.handlers[commandType];

            await handlerAsync(command);
        }

        public void Register<TCommand>(IAsyncCommandHandler<TCommand> handler) where TCommand : ICommand
        {
            var commandType = typeof(TCommand);

            if (handlers.ContainsKey(commandType))
                throw new CommandHandlerAlreadyRegisteredException(commandType);

            handlers.Add(commandType, (Func<TCommand, Task>)handler.HandleAsync);
        }
    }
}