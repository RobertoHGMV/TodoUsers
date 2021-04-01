using FluentValidation;

namespace TodoUsers.Domain.ValueObjects.EmailObj
{
    public class EmailValidator : AbstractValidator<Email>
    {
        public EmailValidator()
        {
            RuleFor(email => email.Address)
                .NotNull().WithMessage("E-mail não informado")
                .EmailAddress().WithMessage("E-mail inválido");
        }
    }
}
