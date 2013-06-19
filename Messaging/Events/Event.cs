namespace Infrastructure.Messaging.Events
{
    using System;

    public abstract class Event : IEvent
    {
        public const int DefaultVersion = -1;
        protected Event()
        {
            EventId = Guid.NewGuid();
            EventTimestamp = DateTime.UtcNow;
            Version = DefaultVersion;
        }
        public Guid EventId
        {
            get;
            set;
        }

        public DateTime EventTimestamp
        {
            get;
            set;
        }

        public int Version
        {
            get;
            set;
        }

        public override string ToString()
        {
            return String.Format("EventTimestamp:{0} EventId:{1} Version:{2}", EventTimestamp, EventId, Version);
        }
    }
}
