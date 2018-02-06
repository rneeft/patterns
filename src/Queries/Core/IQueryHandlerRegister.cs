namespace Chroomsoft.Queries
{
    public interface IQueryHandlerRegister
    {
        TResult Handle<TResult>(IQuery<TResult> query);
        void Register<TQuery, TQueryResult>(IQueryHandler<TQuery, TQueryResult> handler) where TQuery : IQuery<TQueryResult>;
    }
}
