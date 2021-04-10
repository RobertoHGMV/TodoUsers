using FluentValidation;

namespace TodoUsers.Domain.ValueObjects.LoginObj
{
    public class LoginValidator : AbstractValidator<Login>
    {
        public LoginValidator()
        {
            RuleFor(login => login.UserName)
                .NotNull().WithMessage("Nome de usuário não informado")
                .Length(3, 20).WithMessage("Nome de usuário deve ter de 3 a 20 caracteres.");

            RuleFor(login => login.Password)
                .NotNull().WithMessage("Senha não informada")
                .Length(3, 20).WithMessage("Senha deve ter de 3 a 10 caracteres.")
                .Equal(login => login.ConfirmPassword).WithMessage("Senha e confirmação de senha não coincidem");
        }
    }
}
