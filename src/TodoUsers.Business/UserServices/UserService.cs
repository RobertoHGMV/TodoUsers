using System.Collections.Generic;
using TodoUsers.Domain.Commands.Inputs.Users;
using TodoUsers.Domain.Models.Users;
using TodoUsers.Domain.Repositories;
using TodoUsers.Domain.Services;
using TodoUsers.Domain.ValueObjects.EmailObj;
using TodoUsers.Domain.ValueObjects.LoginObj;
using TodoUsers.Domain.ValueObjects.NameObj;
using TodoUsers.Infra.Transactions;

namespace TodoUsers.Business.UserServices
{
    public class UserService : IUserService
    {
        IUserRepository _respository;
        IUow _uow;

        public UserService(IUserRepository respository, IUow uow)
        {
            _respository = respository;
            _uow = uow;
        }

        public IList<User> GetAll()
        {
            return _respository.GetAll();
        }

        public User GetByUserName(string userName)
        {
            return _respository.GetByUserName(userName);
        }

        public User Add(AddUserInput userInput)
        {
            var name = new Name(userInput.FirstName, userInput.LastName);
            var login = new Login(userInput.UserName, userInput.Password, userInput.ConfirmPassword);
            var email = new Email(userInput.Email);
            var user = new User(name, login, email);

            if (user.Invalid) return user;

            _respository.Add(user);
            _uow.Commit();
            return user;
        }

        public User Update(UpdateUserInput userInput)
        {
            var user = GetByUserName(userInput.UserName);
            var name = new Name(userInput.FirstName, userInput.LastName);
            var email = new Email(userInput.Email);

            if (user is null || user.Invalid) return user;

            user.Update(name, email);
            _uow.Commit();
            return user;
        }

        public User Remove(string userName)
        {
            var user = GetByUserName(userName);

            if (user is null) return user;

            _respository.Remove(user);
            _uow.Commit();

            return user;
        }
    }
}
