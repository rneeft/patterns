using System;
using Chroomsoft.Queries;

namespace Example
{
    public class SumQuery : IQuery<int>
    {
        public int A { get; set; }
        public int B { get; set; }
    }

    public class SumQueryHandler : IQueryHandler<SumQuery, int>
    {
        public int Handle(SumQuery query)
        {
            return query.A + query.B;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Setup classes
            IQueryHandlerRegister register = new QueryHandlerRegister();
            register.Register(new SumQueryHandler());

            // Construct Query
            var query = new SumQuery { A = 5, B = 2 };

            // Handle query
            var result = register.Handle(query);

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
