using System;

namespace Chroomsoft.Commands
{
    public class CommandHandlerNotFoundException : Exception
    {
        public CommandHandlerNotFoundException(Type commandType)
        {
            this.CommandType = commandType;
        }

        public Type CommandType { get; }
    }
}
