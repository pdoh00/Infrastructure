namespace Infrastructure.Messaging.Commands
{
    using System.Collections.Generic;
    using System.Linq;

    public class MemoryCommandBus : ICommandBus, ICommandHandlerRegistry
    {
        private List<ICommandHandler> _handlers = new List<ICommandHandler>();
        private List<ICommand> _commands = new List<ICommand>();

        public MemoryCommandBus(params ICommandHandler[] handlers)
        {
            this._handlers.AddRange(handlers);
        }

        public void Register(ICommandHandler handler)
        {
            this._handlers.Add(handler);
        }

        public void Send(ICommand command)
        {
            this._commands.Add(command);

            //May have to use an envelope here to ensure commands stay ordered if we ever
            //go asynch.

            //Task.Factory.StartNew(() =>
            //{
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());

            foreach (dynamic handler in this._handlers
                .Where(x => handlerType.IsAssignableFrom(x.GetType())))
            {
                handler.Handle((dynamic)command);
            }
            //});
        }

        public void Send(IEnumerable<ICommand> commands)
        {
            foreach (var @event in commands)
            {
                this.Send(@event);
            }
        }

        public IEnumerable<ICommand> Events { get { return this._commands; } }
    }
}
