using paymentContext.Shared.Commands;

namespace paymentContext.Shared.Handlers
{
    public interface IHandler<T> where T : ICommand
    {
        ICommandResult Handle(T command);
    }
}