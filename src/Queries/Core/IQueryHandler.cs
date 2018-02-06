namespace Chroomsoft.Queries
{
    public interface IQueryHandler<TQuery, TReadModel> where TQuery : IQuery<TReadModel>
    {
        TReadModel Handle(TQuery query);
    }
}
