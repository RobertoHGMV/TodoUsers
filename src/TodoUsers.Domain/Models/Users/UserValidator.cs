using FluentValidation;
using TodoUsers.Domain.ValueObjects.EmailObj;
using TodoUsers.Domain.ValueObjects.LoginObj;
using TodoUsers.Domain.ValueObjects.NameObj;

namespace TodoUsers.Domain.Models.Users
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Name).SetValidator(c => new NameValidator());
            RuleFor(user => user.Email).SetValidator(c => new EmailAddressValidator());
            RuleFor(user => user.Login).SetValidator(c => new LoginValidator());
        }
    }
}
