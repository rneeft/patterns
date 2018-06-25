using System.Threading.Tasks;

namespace Chroomsoft.Queries
{
    public interface IQueryHandler<TQuery, TReadModel> where TQuery : IQuery<TReadModel>
    {
        Task<TReadModel> HandleAsync(TQuery query);
    }
}