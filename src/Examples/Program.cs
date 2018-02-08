﻿using System;
using System.IO;
using Chroomsoft.Queries;

namespace Example
{
    public class FilesQuery : IQuery<FileInfo[]>
    {
        public string Path { get; set; }
    }

    public class FilesQueryHandler : IQueryHandler<FilesQuery, FileInfo[]>
    {
        public FileInfo[] Handle(FilesQuery query)
        {
            var directory = new DirectoryInfo(query.Path);
            return directory.GetFiles();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Setup classes
            IQueryHandlerRegister register = new QueryHandlerRegister();
            register.Register(new FilesQueryHandler());

            // Construct Query
            var query = new FilesQuery { Path = @"C:\" };

            // Handle query
            var result = register.Handle(query);

            foreach (var file in result)
            {
                Console.WriteLine(file.Name);
            }

            Console.ReadKey();
        }
    }
}