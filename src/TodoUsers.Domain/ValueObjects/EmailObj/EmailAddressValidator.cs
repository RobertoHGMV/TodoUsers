using FluentValidation;

namespace TodoUsers.Domain.ValueObjects.EmailObj
{
    public class EmailAddressValidator : AbstractValidator<Email>
    {
        public EmailAddressValidator()
        {
            RuleFor(email => email.Address)
                .NotNull().WithMessage("E-mail não informado")
                .EmailAddress().WithMessage("E-mail inválido");
        }
    }
}
