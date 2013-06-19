namespace Infrastructure.Messaging.Events
{
    using System;

    public interface IEvent
    {
        Guid EventId { get; }
        DateTime EventTimestamp { get; }
        int Version { get; set; }
    }
}
