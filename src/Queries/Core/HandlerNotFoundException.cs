using System;

namespace Chroomsoft.Queries
{
    public class HandlerNotFoundException : Exception
    {
        public HandlerNotFoundException(Type type)
        {
            this.Type = type;
        }

        public Type Type { get; }
    }
}
