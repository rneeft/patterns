# Command Pattern

Package that provides the foundation to implement the Commands Pattern

## How to install

`Install-Package Chroomsoft.Commands`

[NuGet.org](https://www.nuget.org/packages/Chroomsoft.Commands/)

## How to use

- Create a (.NET Core) console application
- Install the `Chroomsoft.Commands` package
- Pasted the following code:

```csharp
using System;
using System.IO;
using Chroomsoft.Commands;

namespace Example
{
    public class DeleteCommand : ICommand
    {
        public string File {get; set;}
    }

    public class DeleteCommandHandler : ICommandHandler<DeleteCommand>
    {
        public void Handle(DeleteCommand command)
        {
            File.Delete(command.File);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Setup classes
            ICommandHandlerRegister register = new CommandRegister();
            register.Register(new DeleteCommandHandler());

            // Construct Command
            var command = new DeleteCommand { File = @"C:\Example.txt" };

            // Handle Command
            register.Handle(command)

            Console.ReadKey();
        }
    }
}
```
