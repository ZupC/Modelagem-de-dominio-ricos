using paymentContext.Domain.Entities;
using paymentContext.Domain.Enums;
using paymentContext.Domain.ValueObjects;

namespace paymentContext.tests
{
    [TestClass]
    public class StudentTests
    {
        private readonly Name _name;
        private readonly Email _email;
        private readonly Document _document;
        private readonly Address _address;
        private readonly Student _student;

        public StudentTests()
        {
            _name = new Name("Cairo", "S Zupirolli");
            _document = new Document("30605259003", EDocumentType.CPF);
            _email = new Email("cairosouza1@gmail.com");
            _address = new Address("Rua adalberto caceres gil", "468", "centro", "Novais", "SP", "Brasil", "15885-155");
            _student = new Student(_name, _document, _email);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription()
        {
            var subscription = new Subscription(null);
            var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Cairo Corp", _document, _address, _email);

            subscription.AddPayment(payment);
            _student.AddSubscription(subscription);
            _student.AddSubscription(subscription);

            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscriptionHasNoPayment()
        {
            var subscription = new Subscription(null);

            _student.AddSubscription(subscription);

            Assert.IsTrue(_student.Invalid);
        }
        [TestMethod]
        public void ShouldReturnSuccessWhenHadAddSubscription()
        {
            var subscription = new Subscription(null);
            var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Cairo Corp", _document, _address, _email);

            subscription.AddPayment(payment);
            _student.AddSubscription(subscription);

            Assert.IsTrue(_student.Valid);
        }
    }
}