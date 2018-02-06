using System;

namespace Chroomsoft.Queries.Tests
{
    public class TestQueryHandler : IQueryHandler<TestQuery, Guid>
    {
        private readonly Guid guid;

        public TestQueryHandler(Guid guid)
        {
            this.guid = guid;
        }

        public Guid Handle(TestQuery query)
        {
            return guid;
        }
    }
}
