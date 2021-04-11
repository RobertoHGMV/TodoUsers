using TodoUsers.Domain.Commands.Inputs.Users;
using TodoUsers.Domain.Models.Users;
using TodoUsers.Domain.ValueObjects.EmailObj;
using TodoUsers.Domain.ValueObjects.LoginObj;
using TodoUsers.Domain.ValueObjects.NameObj;

namespace TodoUsers.Tests.Helpers
{
    public class UserHelper
    {
        public readonly User UserValid = new User(
            new Name("Sebastian", "Vetel"),
            new Login("vet", "vet@123", "vet@123"),
            new Email("vet@gmail.com"));

        public User UserInvalid = new User(new Name("", ""), new Login("", "", ""), new Email(""));

        public readonly AddUserInput AddUserInputValid = new AddUserInput
        {
            FirstName = "Sebastian",
            LastName = "Vetel",
            UserName = "vet",
            Password = "12345",
            ConfirmPassword = "12345",
            Email = "vet@gmail.com"
        };

        public readonly UpdateUserInput UpdateUserInputValid = new UpdateUserInput
        {
            FirstName = "Sebastian",
            LastName = "Vetel",
            UserName = "vet",
            Email = "vet@gmail.com"
        };

        public readonly AddUserInput AddUserInputInvalid = new AddUserInput();
        public readonly UpdateUserInput UpdateUserInputInvalid = new UpdateUserInput();
    }
}
