namespace Infrastructure.Messaging.Commands
{
    public interface ICommandHandlerRegistry
    {
        void Register(ICommandHandler handler);
    }
}
