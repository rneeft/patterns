using System.Threading.Tasks;

namespace Chroomsoft.Queries
{
    public abstract class QuerySyncHandler<TQuery, TReadModel> : IQueryHandler<TQuery, TReadModel> where TQuery : IQuery<TReadModel>
    {
        public Task<TReadModel> HandleAsync(TQuery query)
        {
            var result = Handle(query);

            return Task.FromResult(result);
        }

        protected abstract TReadModel Handle(TQuery query);
    }
}