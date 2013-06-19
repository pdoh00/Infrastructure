namespace Infrastructure.DDD
{
    using System;
    using System.Collections.Generic;
    using Infrastructure.Messaging.Events;

    public interface IAggregateRoot
    {
        Guid Id { get; }
        int Version { get; }
        void Apply(IEvent @event);
        void Apply(IEnumerable<IEvent> events);
        void ClearUncommitedChanges();
        List<IEvent> GetUncommitedChanges();
    }
}
