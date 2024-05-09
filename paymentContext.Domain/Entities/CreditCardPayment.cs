using paymentContext.Domain.ValueObjects;

namespace paymentContext.Domain.Entities
{
    public class CreditCardPayment : Payment
    {
        public CreditCardPayment(
            string cardHolderName,
            string cardLastsNumber,
            string lastTransactionNumber,
            DateTime date,
            DateTime expireDate,
            decimal total,
            decimal totalPaid,
            string payer,
            Document document,
            Address address,
            Email email)
            : base(date, expireDate, total, totalPaid, payer, document, address, email)
        {
            CardHolderName = cardHolderName;
            CardLastsNumber = cardLastsNumber;
            LastTransactionNumber = lastTransactionNumber;
        }

        public string CardHolderName { get; private set; }
        public string CardLastsNumber { get; private set; }
        public string LastTransactionNumber { get; private set; }
    }
}