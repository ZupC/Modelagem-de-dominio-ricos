using Flunt.Notifications;

namespace paymentContext.Shared.Entities
{
    public abstract class Entitiy : Notifiable
    {
        protected Entitiy()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
    }
}