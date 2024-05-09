using Flunt.Validations;
using paymentContext.Shared.ValueObjetcs;

namespace paymentContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            AddNotifications(new Contract()
            .Requires()
            .HasMinLen(FirstName, 3, "Name.FirstName", "Nome vazio ou não preenchido")
            .HasMinLen(LastName, 3, "Name.LastName", "Sobrenome vazio ou não preenchido")
            .HasMaxLen(FirstName, 40, "Name.FirstName", "Nome vazio ou não preenchido")
            );
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}