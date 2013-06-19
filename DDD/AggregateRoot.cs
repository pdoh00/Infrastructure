namespace Infrastructure.DDD
{
    using System;
    using System.Collections.Generic;
    using Infrastructure.Messaging.Events;

    public abstract class AggregateRoot : IAggregateRoot
    {
        private const int DefaultVersion = 0;
        private readonly Dictionary<Type, Action<IEvent>> handlers = new Dictionary<Type, Action<IEvent>>();
        private List<IEvent> _uncommitedChanges = new List<IEvent>();

        public Guid Id { get; protected set; }

        public int Version { get; private set; }

        public AggregateRoot()
        {
            Version = DefaultVersion;
        }

        public void Apply(IEvent @event)
        {
            if (@event.Version == Event.DefaultVersion)
                _uncommitedChanges.Add(@event);

            @event.Version = this.Version + 1;
            this.handlers[@event.GetType()].Invoke(@event);

            ++Version;
        }

        public void Apply(IEnumerable<IEvent> @events)
        {
            foreach (var @event in @events)
            {
                Apply(@event);
            }
        }

        public List<IEvent> GetUncommitedChanges()
        {
            return _uncommitedChanges;
        }

        public void ClearUncommitedChanges()
        {
            _uncommitedChanges.Clear();
        }

        protected void Handles<TEvent>(Action<TEvent> handler)
            where TEvent : IEvent
        {
            this.handlers.Add(typeof(TEvent), @event => handler((TEvent)@event));
        }
    }
}
