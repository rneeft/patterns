using System.Threading.Tasks;

namespace Chroomsoft.Queries
{
    public interface IQueryHandlerRegister
    {
        Task<TQueryResult> HandleAsync<TQueryResult>(IQuery<TQueryResult> query);

        void Register<TQuery, TQueryResult>(IQueryHandler<TQuery, TQueryResult> handler) where TQuery : IQuery<TQueryResult>;
    }
}