using Flunt.Notifications;
using Flunt.Validations;
using paymentContext.Domain.Commands;
using paymentContext.Domain.Entities;
using paymentContext.Domain.Enums;
using paymentContext.Domain.Repositories;
using paymentContext.Domain.Services;
using paymentContext.Domain.ValueObjects;
using paymentContext.Shared.Commands;
using paymentContext.Shared.Handlers;

namespace paymentContext.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable, IHandler<CreateBoletoSubscriptionCommand>, IHandler<CreatePayPalSubscriptionCommand>, IHandler<CreateCreditCardSubscriptionCommand>
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;
        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }
        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            // Fail fast validation
            command.Validate();
            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar a assinatura");
            }

            // Verificar se o documento já está cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF/CNPJ já está em uso");

            // Verificar se E-mail já está cadastrado
            if (_repository.EmailExists(command.Document))
                AddNotification("Email", "Este E-mail já está em uso");

            // Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

            // Gerar as entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(
                command.BarCode, command.BoletoNumber, command.PaidDate, command.ExpireDate,
                command.Total, command.TotalPaid, command.Payer, new Document(command.PayerDocument, command.PayerDocumentType),
                address, email
            );

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar as validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            //Salvar as informações
            if (Invalid)
            {
                return new CommandResult(false, "não foi possivel realizar a sua assinatura");
            }

            // Salvar as informações
            _repository.CreateSubscription(student);

            // Enviar E-mail de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo ao Zup.io", "Sua assinatura foi criada");

            // Retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso");
        }

        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {
            // Fail fast validation
            command.Validate();
            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar a assinatura");
            }

            // Verificar se o documento já está cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF/CNPJ já está em uso");

            // Verificar se E-mail já está cadastrado
            if (_repository.EmailExists(command.Document))
                AddNotification("Email", "Este E-mail já está em uso");

            // Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

            // Gerar as entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PayPalPayment(
                command.TransactionCode, command.PaidDate, command.ExpireDate,
                command.Total, command.TotalPaid, command.Payer, new Document(command.PayerDocument, command.PayerDocumentType),
                address, email
            );

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar as validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            // Salvar as informações
            _repository.CreateSubscription(student);

            // Enviar E-mail de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo ao Zup.io", "Sua assinatura foi criada");

            // Retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso");
        }

        public ICommandResult Handle(CreateCreditCardSubscriptionCommand command)
        {
            // Fail fast validation
            command.Validate();
            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar a assinatura");
            }

            // Verificar se o documento já está cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF/CNPJ já está em uso");

            // Verificar se E-mail já está cadastrado
            if (_repository.EmailExists(command.Document))
                AddNotification("Email", "Este E-mail já está em uso");

            // Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

            // Gerar as entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new CreditCardPayment(
                command.CardHolderName, command.CardLastsNumber, command.LastTransactionNumber, command.PaidDate, command.ExpireDate,
                command.Total, command.TotalPaid, command.Payer, new Document(command.PayerDocument, command.PayerDocumentType),
                address, email
            );

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar as validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            // Salvar as informações
            _repository.CreateSubscription(student);

            // Enviar E-mail de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo ao Zup.io", "Sua assinatura foi criada");

            // Retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso");
        }
    }
}