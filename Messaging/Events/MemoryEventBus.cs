namespace Infrastructure.Messaging.Events
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;

    public class MemoryEventBus : IEventBus, IEventHandlerRegistry
    {
        private List<IEventHandler> handlers = new List<IEventHandler>();
        //private List<IEvent> events = new List<IEvent>();
        string fileOut;
        System.Threading.ReaderWriterLockSlim writeLock = new ReaderWriterLockSlim();

        public MemoryEventBus(params IEventHandler[] handlers)
        {
            this.handlers.AddRange(handlers);

            fileOut = string.Format("F:\\Temp\\{0}.{1}.txt", "MemoryEventBusPublished", Guid.NewGuid());
            if (!File.Exists(fileOut))
            {
                var stream = File.Create(fileOut);
                stream.Close();
            }
        }

        public void Register(IEventHandler handler)
        {
            this.handlers.Add(handler);
        }

        public void Publish(IEvent @event)
        {
            //this.events.Add(@event);
            WriteEventToFile(@event);

            //Task.Factory.StartNew(() =>
            //{
                var handlerType = typeof(IEventHandler<>).MakeGenericType(@event.GetType());

                foreach (dynamic handler in this.handlers.Where(x => handlerType.IsAssignableFrom(x.GetType())))
                {
                    handler.Handle((dynamic)@event);
                }
            //});
        }

        public void Publish(IEnumerable<IEvent> events)
        {
            foreach (var @event in events)
            {
                this.Publish(@event);
            }
        }


        private void WriteEventToFile(IEvent @event)
        {

            writeLock.EnterWriteLock();
            try
            {
                using (var w = new StreamWriter(fileOut, true))
                {
                    w.WriteLine(@event.ToString());
                    w.Close();
                    Console.WriteLine(@event.ToString());
                }
            }
            finally
            {
                writeLock.ExitWriteLock();
            }
        }
        //public IEnumerable<IEvent> Events { get { return this.events; } }
    }
}
