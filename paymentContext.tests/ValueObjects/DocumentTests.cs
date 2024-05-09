using paymentContext.Domain.Enums;
using paymentContext.Domain.ValueObjects;

namespace tests
{
    [TestClass]
    public class DocumentTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenCNPJIsInvalid()
        {
            var doc = new Document("123", EDocumentType.CNPJ);
            Assert.IsTrue(doc.Invalid);
        }
        [TestMethod]
        public void ShouldReturnSuccessWhenCNPJIsValid()
        {
            var doc = new Document("44926847000177", EDocumentType.CNPJ);
            Assert.IsTrue(doc.Valid);
        }
        [TestMethod]
        public void ShouldReturnErrorWhenCPFIsInvalid()
        {
            var doc = new Document("123", EDocumentType.CPF);
            Assert.IsTrue(doc.Invalid);
        }
        [TestMethod]
        public void ShouldReturnSuccessWhenCPFIsValid()
        {
            var doc = new Document("77579755068", EDocumentType.CPF);
            Assert.IsTrue(doc.Valid);
        }
    }
}