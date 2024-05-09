using paymentContext.Domain.ValueObjects;

namespace paymentContext.Domain.Entities
{
    public class BoletoPayment : Payment
    {
        public BoletoPayment(
            string barCode,
            string boletoNumber,
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
            this.BarCode = barCode;
            this.BoletoNumber = boletoNumber;
        }

        public string BarCode { get; private set; }
        public string BoletoNumber { get; private set; }
    }
}