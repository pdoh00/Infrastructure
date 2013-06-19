namespace Infrastructure.Messaging.Commands
{
    using System;

    public abstract class Command : ICommand
    {
        private readonly Guid _id;
        private readonly DateTime _timeStamp;

        public Command()
        {
            _id = Guid.NewGuid();
            _timeStamp = DateTime.UtcNow;
        }
        public Guid Id
        {
            get { return _id; }
        }

        public DateTime Timestamp
        {
            get { return _timeStamp; }
        }
    }
}
