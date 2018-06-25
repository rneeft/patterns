using System;
using System.Threading.Tasks;

namespace Chroomsoft.Queries.Tests
{
    public class TestQueryHandler : IQueryHandler<TestQuery, Guid>
    {
        private readonly Guid guid;

        public TestQueryHandler(Guid guid)
        {
            this.guid = guid;
        }

        public Task<Guid> HandleAsync(TestQuery query)
        {
            return Task.FromResult(guid);
        }
    }
}