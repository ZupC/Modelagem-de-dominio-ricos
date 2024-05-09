using paymentContext.Domain.Entities;
using paymentContext.Domain.Repositories;
using paymentContext.Domain.Services;

namespace paymentContext.tests.Mocks
{
    public class FakeEmailService : IEmailService
    {
        public void Send(string to, string email, string subject, string body)
        {
        }
    }
}