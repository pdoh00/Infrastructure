namespace Infrastructure.Messaging.Commands
{
    using System;

    public interface ICommand
    {
        Guid Id { get; }
        DateTime Timestamp { get; }
    }
}
