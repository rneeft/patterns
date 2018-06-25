using System;
using System.Threading.Tasks;

namespace Chroomsoft.Queries.Tests
{
    public class TestQueryAsyncHandler : IQueryHandler<TestQuery, Guid>
    {
        private readonly Guid guid;

        public TestQueryAsyncHandler(Guid guid)
        {
            this.guid = guid;
        }

        public async Task<Guid> HandleAsync(TestQuery query)
        {
            await Task.Delay(1);

            return guid;
        }
    }
}