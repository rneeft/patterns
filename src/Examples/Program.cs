using System;
using System.IO;
using System.Threading.Tasks;
using Chroomsoft.Queries;

namespace Example
{
    public class FilesQuery : IQuery<FileInfo[]>
    {
        public string Path { get; set; }
    }

    public class FileContentQuery : IQuery<string>
    {
        public string FilePath { get; set; }
    }

    public class FilesQueryHandler : QuerySyncHandler<FilesQuery, FileInfo[]>
    {
        protected override FileInfo[] Handle(FilesQuery query)
        {
            var directory = new DirectoryInfo(query.Path);
            return directory.GetFiles();
        }
    }

    public class FileContentQueryHandler : IQueryHandler<FileContentQuery, string>
    {
        public async Task<string> HandleAsync(FileContentQuery query)
        {
            return await File.ReadAllTextAsync(query.FilePath);
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            // Setup classes
            IQueryHandlerRegister register = new QueryHandlerRegister();
            register.Register(new FilesQueryHandler());
            register.Register(new FileContentQueryHandler());

            var files = FilesAsync(register).Result;
            foreach (var file in files)
            {
                Console.WriteLine(file.Name);
            }

            var fileContent = FileContentAsync(register).Result;
            Console.WriteLine(fileContent);

            Console.ReadKey();
        }

        private static async Task<FileInfo[]> FilesAsync(IQueryHandlerRegister register)
        {
            // Construct query
            var query = new FilesQuery
            {
                Path = @"C:\"
            };

            // Handle query
            return await register.HandleAsync(query);
        }

        private static async Task<string> FileContentAsync(IQueryHandlerRegister register)
        {
            // Construct query
            var query = new FileContentQuery
            {
                FilePath = @"C:\myfile.txt"
            };

            // Handle query
            return await register.HandleAsync(query);
        }
    }
}