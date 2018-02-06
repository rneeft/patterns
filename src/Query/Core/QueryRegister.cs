using System;
using System.Collections.Generic;

namespace Chroomsoft.Queries
{
    public class QueryHandlerRegister : IQueryHandlerRegister
    {
        private readonly Dictionary<Type, object> handlers = new Dictionary<Type, object>();

        public void Register<TQuery, TQueryResult>(IQueryHandler<TQuery, TQueryResult> handler) where TQuery : IQuery<TQueryResult>
        {
            var queryType = typeof(TQuery);

            if (handlers.ContainsKey(queryType))
                throw new HandlerAlreadyRegisteredException(queryType);

            handlers.Add(typeof(TQuery), (Func<TQuery, TQueryResult>)handler.Handle);
        }

        public TResult Handle<TResult>(IQuery<TResult> query)
        {
            var type = query.GetType();

            if (!handlers.ContainsKey(type))
                throw new HandlerNotFoundException(type);

            dynamic handler = this.handlers[query.GetType()];

            return handler((dynamic)query);
        }
    }
}
