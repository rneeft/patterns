namespace Chroomsoft.Commands.Test
{
    public class ExceptionSyncCommandHandler : CommandSyncHandler<ExceptionTestCommand>
    {
        protected override void Handle(ExceptionTestCommand command)
        {
            throw new TestException();
        }
    }
}