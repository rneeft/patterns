using System;

namespace Chroomsoft.Queries
{
    public class HandlerAlreadyRegisteredException : Exception
    {
        public HandlerAlreadyRegisteredException(Type type)
        {
            this.Type = type;
        }

        public Type Type { get; }
    }
}
