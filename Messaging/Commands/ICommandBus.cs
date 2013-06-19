namespace Infrastructure.Messaging.Commands
{
    using System.Collections.Generic;

    public interface ICommandBus
    {
        void Send(ICommand command);
        void Send(IEnumerable<ICommand> commands);
    }
}
