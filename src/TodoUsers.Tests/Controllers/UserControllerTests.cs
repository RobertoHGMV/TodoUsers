using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TodoUsers.Api.Rest.Controllers;
using TodoUsers.Domain.Services;
using TodoUsers.Tests.Helpers;

namespace TodoUsers.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTests
    {
        private Mock<IUserService> _userServiceMock;
        private UserHelper _userHelper;

        [SetUp]
        public void Setup()
        {
            _userServiceMock = new Mock<IUserService>();
            _userHelper = new UserHelper();
        }

        [Test]
        public void DeveRetornarActionResultOkAoObterUsuarios()
        {
            var sut = new UserController(_userServiceMock.Object);

            var result = sut.Get() as OkObjectResult;

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void DeveRetornarActionResultOkAoObterUsuarioValido()
        {
            _userServiceMock.Setup(serv => serv.GetByUserName("vet")).Returns(_userHelper.UserValid);
            var sut = new UserController(_userServiceMock.Object);

            var result = sut.Get("vet") as OkObjectResult;

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void DeveRetornarActionResultNotFoundAoObterUsuarioInvalido()
        {
            var sut = new UserController(_userServiceMock.Object);

            var result = sut.Get("") as NotFoundObjectResult;

            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void DeveRetornarActionResultOkAoAdicionarUsuarioValido()
        {
            _userServiceMock.Setup(serv => serv.Add(_userHelper.AddUserInputValid)).Returns(_userHelper.UserValid);
            var sut = new UserController(_userServiceMock.Object);

            var result = sut.Add(_userHelper.AddUserInputValid) as CreatedResult;

            Assert.IsInstanceOf<CreatedResult>(result);
        }

        [Test]
        public void DeveRetornarActionResultBadRequestAoAdicionarUsuarioInvalido()
        {
            _userServiceMock.Setup(serv => serv.Add(_userHelper.AddUserInputInvalid)).Returns(_userHelper.UserInvalid);
            var sut = new UserController(_userServiceMock.Object);

            var result = sut.Add(_userHelper.AddUserInputInvalid) as BadRequestObjectResult;

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void DeveRetornarActionResultOkAoAtualizarUsuarioValido()
        {
            _userServiceMock.Setup(serv => serv.Update(_userHelper.UpdateUserInputValid)).Returns(_userHelper.UserValid);
            var sut = new UserController(_userServiceMock.Object);

            var result = sut.Update(_userHelper.UpdateUserInputValid) as OkObjectResult;

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void DeveRetornarActionResultBadRequestAoAtualizarUsuarioInvalido()
        {
            _userServiceMock.Setup(serv => serv.Update(_userHelper.UpdateUserInputInvalid)).Returns(_userHelper.UserInvalid);
            var sut = new UserController(_userServiceMock.Object);

            var result = sut.Update(_userHelper.UpdateUserInputInvalid) as BadRequestObjectResult;

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void DeveRetornarActionResultNotFoundAoAtualizarUsuarioInvalido()
        {
            var sut = new UserController(_userServiceMock.Object);

            var result = sut.Update(_userHelper.UpdateUserInputInvalid) as NotFoundObjectResult;

            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void DeveRetornarActionResultNotContentAoRemoverUsuarioValido()
        {
            _userServiceMock.Setup(serv => serv.Remove("vet")).Returns(_userHelper.UserValid);
            var sut = new UserController(_userServiceMock.Object);

            var result = sut.Delete("vet") as NoContentResult;

            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public void DeveRetornarActionResultNotFoundAoRemoverUsuarioInValido()
        {
            var sut = new UserController(_userServiceMock.Object);

            var result = sut.Delete("") as NotFoundObjectResult;

            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }
    }
}
