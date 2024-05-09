using paymentContext.Domain.Entities;

namespace paymentContext.Domain.Repositories
{
    public interface IStudentRepository
    {
        bool DocumentExists(string document);
        bool EmailExists(string email);

        void CreateSubscription(Student student);
    }
}