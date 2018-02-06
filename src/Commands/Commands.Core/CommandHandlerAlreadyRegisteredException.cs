using System;

namespace Chroomsoft.Commands
{
    public class CommandHandlerAlreadyRegisteredException : Exception
    {
        public CommandHandlerAlreadyRegisteredException(Type handlerType)
        {
            this.HandlerType = handlerType;
        }

        public Type HandlerType { get; }
    }
}
