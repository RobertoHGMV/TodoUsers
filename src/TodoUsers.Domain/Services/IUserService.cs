using System.Collections.Generic;
using TodoUsers.Domain.Commands.Inputs.Users;
using TodoUsers.Domain.Models.Users;

namespace TodoUsers.Domain.Services
{
    public interface IUserService
    {
        IList<User> GetAll();
        User GetByUserName(string userName);
        User Add(AddUserInput userInput);
        User Update(UpdateUserInput userInput);
        User Remove(string userName);
    }
}
