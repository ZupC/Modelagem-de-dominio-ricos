using Flunt.Validations;
using paymentContext.Domain.Enums;
using paymentContext.Shared.ValueObjetcs;

namespace paymentContext.Domain.ValueObjects
{
    public class Document : ValueObject
    {
        public Document(string number, EDocumentType type)
        {
            Number = number;
            Type = type;

            AddNotifications(new Contract()
                .Requires()
                .IsTrue(Validade(), "Document.Number", "Documento inválido")
            );
        }

        public string Number { get; private set; }
        public EDocumentType Type { get; private set; }

        private bool Validade()
        {
            if (Type == EDocumentType.CNPJ && Number.Length == 14)
                return true;

            if (Type == EDocumentType.CPF && Number.Length == 11)
                return true;

            return false;
        }
    }
}