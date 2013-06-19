namespace Infrastructure.Messaging.Events
{
    using System.Collections.Generic;

    public interface IEventBus
    {
        void Publish(IEvent @event);
        void Publish(IEnumerable<IEvent> events);
    }
}
