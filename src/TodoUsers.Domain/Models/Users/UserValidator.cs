using FluentValidation;

namespace TodoUsers.Domain.Models.Users
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Name).SetValidator(c => c.Name.Validator);
            RuleFor(user => user.Email).SetValidator(c => c.Email.Validator);
            RuleFor(user => user.Login).SetValidator(c => c.Login.Validator);
        }
    }
}
