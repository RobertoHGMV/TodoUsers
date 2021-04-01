using FluentValidation;

namespace TodoUsers.Domain.ValueObjects.NameObj
{
    public class NameValidator : AbstractValidator<Name>
    {
        public NameValidator()
        {
            RuleFor(name => name.FirstName)
                .NotNull().WithMessage("Nome não informado")
                .Length(3, 60).WithMessage("Nome deve ter de 03 a 60 caracteres");

            RuleFor(name => name.LastName)
                .NotEmpty().WithMessage("Por favor, informe o sobrenome")
                .Length(3, 60).WithMessage("Sobrenome deve ter de 03 a 60 caracteres");
        }
    }
}
