using Flunt.Validations;
using paymentContext.Domain.ValueObjects;
using paymentContext.Shared.Entities;

namespace paymentContext.Domain.Entities
{
    public class Student : Entitiy
    {
        private IList<Subscription> _subscriptions;
        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;
            _subscriptions = new List<Subscription>();

            AddNotifications(name, document, email);
        }

        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }

        public IReadOnlyCollection<Subscription> Subscriptions { get { return _subscriptions.ToArray(); } }

        public void AddSubscription(Subscription subscription)
        {
            var hasSubscriptionActive = false;
            foreach (var sub in _subscriptions)
            {
                if (sub.Active)
                    hasSubscriptionActive = true;
            }

            AddNotifications(new Contract()
                .Requires()
                .IsFalse(hasSubscriptionActive, "Student.Subscriptions", "Você já possui uma assinatura ativa")
                .AreNotEquals(0, subscription.Payments.Count(), "Student.Subscriptions.Payments", "Está assinatura não possui pagamentos")
            );

            if (Valid)
            {
                _subscriptions.Add(subscription);
            }
        }
    }
}