namespace Infrastructure.Messaging.Events
{
    public interface IEventHandlerRegistry
    {
        void Register(IEventHandler handler);
    }
}
