using System;
using System.IO;
using System.Threading.Tasks;

namespace Chroomsoft.Commands.Example
{
    public class DeleteCommand : ICommand
    {
        public string FileName { get; set; }
    }

    public class CreateFileCommand : ICommand
    {
        public string FileName { get; set; }

        public string Content { get; set; }
    }

    public class DeleteCommandHandler : CommandSyncHandler<DeleteCommand>
    {
        protected override void Handle(DeleteCommand command)
        {
            File.Delete(command.FileName);
        }
    }

    public class CreateFileCommandHandler : IAsyncCommandHandler<CreateFileCommand>
    {
        public async Task HandleAsync(CreateFileCommand command)
        {
            await File.WriteAllTextAsync(command.FileName, command.Content);
        }
    }

    internal class Program
    {
        private static void CommandMain(string[] args)
        {
            // Setup classes
            ICommandHandlerRegister register = new CommandRegister();
            register.Register(new DeleteCommandHandler());
            register.Register(new CreateFileCommandHandler());

            CreateFileAsync(register).Wait();
            DeleteFile(register).Wait();

            Console.ReadKey();
        }

        private static async Task CreateFileAsync(ICommandHandlerRegister register)
        {
            // Construct command
            var command = new CreateFileCommand
            {
                FileName = "MyFile.txt",
                Content = "Hello Command"
            };

            // Execute the command
            await register.ExecuteAsync(command);
        }

        private static async Task DeleteFile(ICommandHandlerRegister register)
        {
            // Construct Command
            var command = new DeleteCommand { FileName = "MyFile.txt" };

            // Handle Command
            await register.ExecuteAsync(command);
        }
    }
}