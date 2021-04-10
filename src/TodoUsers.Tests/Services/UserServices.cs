using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoUsers.Business.UserServices;
using TodoUsers.Domain.Commands.Inputs.Users;
using TodoUsers.Domain.Models.Users;
using TodoUsers.Domain.Repositories;
using TodoUsers.Domain.Services;
using TodoUsers.Domain.ValueObjects.EmailObj;
using TodoUsers.Domain.ValueObjects.LoginObj;
using TodoUsers.Domain.ValueObjects.NameObj;
using TodoUsers.Infra.Transactions;

namespace TodoUsers.Tests.Services
{
    [TestFixture]
    public class UserServices
    {
        private Mock<IUow> _uowMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private IUserService _userService;

        private User _userValid = new User(
            new Name("Sebastian", "Vetel"), 
            new Login("vet", "vet@123", "vet@123"), 
            new Email("vet@gmail.com"));

        private User _userInvalid = new User(new Name("", ""), new Login("", "", ""), new Email(""));

        private AddUserInput _addUserInputValid = new AddUserInput 
        {
            FirstName = "Sebastian",
            LastName = "Vetel",
            UserName = "vet",
            Password = "12345",
            ConfirmPassword = "12345",
            Email = "vet@gmail.com"
        };

        private UpdateUserInput _updateUserInputValid = new UpdateUserInput
        {
            FirstName = "Sebastian",
            LastName = "Vetel",
            UserName = "vet",
            Email = "vet@gmail.com"
        };

        private AddUserInput _addUserInputInvalid = new AddUserInput();

        [SetUp]
        public void Setup()
        {
            _uowMock = new Mock<IUow>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_userRepositoryMock.Object, _uowMock.Object);
        }

        [Test]
        public void DadoUsuarioValidoParametroValidDeveSerVerdadeiroAoAdicionar()
        {
            var user = _userService.Add(_addUserInputValid);

            Assert.IsTrue(user.Valid);
        }

        [Test]
        public void DadoUsuarioInvalidoParametroInvalidDeveSerVerdadeiroAoAdicionar()
        {
            var user = _userService.Add(_addUserInputInvalid);

            Assert.IsTrue(user.Invalid);
        }

        [Test]
        public void DadoUsuarioValidoParametroValidDeveSerVerdadeiroAoEditar()
        {
            _userRepositoryMock.Setup(user => user.GetByUserName("vet")).Returns(_userValid);
            _userService = new UserService(_userRepositoryMock.Object, _uowMock.Object);

            var user = _userService.Update(_updateUserInputValid);

            Assert.IsTrue(user.Valid);
        }

        [Test]
        public void DadoUsuarioInvalidoParametroValidDeveSerVerdadeiroAoEditar()
        {
            _userRepositoryMock.Setup(user => user.GetByUserName("vet")).Returns(_userInvalid);
            _userService = new UserService(_userRepositoryMock.Object, _uowMock.Object);

            var user = _userService.Update(_updateUserInputValid);

            Assert.IsTrue(user.Invalid);
        }
    }
}
