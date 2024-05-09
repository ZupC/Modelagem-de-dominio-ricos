using Flunt.Validations;
using paymentContext.Shared.Entities;

namespace paymentContext.Domain.Entities
{
    public class Subscription : Entitiy
    {
        private IList<Payment> _payments;
        public Subscription(DateTime? expireDate)
        {
            CreateDate = DateTime.Now;
            LastUpdateDate = DateTime.Now;
            ExpireDate = expireDate;
            Active = true;
            _payments = new List<Payment>();
        }

        public DateTime CreateDate { get; private set; }
        public DateTime LastUpdateDate { get; private set; }
        public DateTime? ExpireDate { get; private set; }
        public bool Active { get; private set; }

        public IReadOnlyCollection<Payment> Payments { get { return _payments.ToArray(); } }

        public void AddPayment(Payment payment)
        {
            AddNotifications(new Contract()
                .Requires()
                .IsGreaterThan(DateTime.Now, payment.Date, "Subscription.Payments", "A data do pagamento deve ser futura")
            );
            _payments.Add(payment);
        }

        public void ActivateSubs(bool activeOption)
        {
            Active = activeOption;
            LastUpdateDate = DateTime.Now;
        }
    }
}