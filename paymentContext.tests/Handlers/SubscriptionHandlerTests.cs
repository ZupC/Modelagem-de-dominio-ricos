using paymentContext.Domain.Commands;
using paymentContext.Domain.Enums;
using paymentContext.Domain.Handlers;
using paymentContext.tests.Mocks;

namespace paymentContext.tests
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());

            var command = new CreateBoletoSubscriptionCommand();

            command.FirstName = "Cairo";
            command.LastName = "S Zupirolli";
            command.Document = "99999999999";
            command.Email = "cairo@cairo.io";
            command.BarCode = "123456789";
            command.BoletoNumber = "1234567489456";
            command.PaymentNumber = "123456";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);
            command.Total = 60;
            command.TotalPaid = 60;
            command.Payer = "ZupCorp";
            command.PayerDocument = "123456789113";
            command.PayerDocumentType = EDocumentType.CPF;
            command.PayerEmail = "corp@zup.com";
            command.Street = "safas";
            command.Number = "fsafas";
            command.Neighborhood = "asffas";
            command.City = "as";
            command.State = "as";
            command.Country = "as";
            command.ZipCode = "1241512512";

            handler.Handle(command);
            Assert.AreEqual(false, handler.Valid);

        }
    }
}