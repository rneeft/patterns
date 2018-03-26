using System;
using System.Threading.Tasks;

namespace Chroomsoft.Queries.Tests
{
    public class TestQueryAsyncHandler : IQueryHandler<TestAsyncQuery, Task<Guid>>
    {
        private readonly Guid guid;

        public TestQueryAsyncHandler(Guid guid)
        {
            this.guid = guid;
        }

        public async Task<Guid> Handle(TestAsyncQuery query)
        {
            await Task.Delay(1);

            return guid;
        }
    }
}
