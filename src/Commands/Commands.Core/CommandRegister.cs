using System;
using System.Collections.Generic;

namespace Chroomsoft.Commands
{
    public class CommandRegister : ICommandHandlerRegister
    {
        private readonly Dictionary<Type, object> handlers = new Dictionary<Type, object>();

        public void Handle<TCommand>(TCommand command)
        {
            var commandType = command.GetType();

            if (!handlers.ContainsKey(commandType))
                throw new CommandHandlerNotFoundException(commandType);

            var handler = (Action<TCommand>)this.handlers[commandType];

            handler(command);
        }

        public void Register<TCommand>(ICommandHandler<TCommand> handler) where TCommand : ICommand
        {
            var commandType = typeof(TCommand);

            if (handlers.ContainsKey(commandType))
                throw new CommandHandlerAlreadyRegisteredException(commandType);

            handlers.Add(typeof(TCommand), (Action<TCommand>)handler.Handle);
        }
    }
}
