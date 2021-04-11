using Moq;
using NUnit.Framework;
using TodoUsers.Business.UserServices;
using TodoUsers.Domain.Repositories;
using TodoUsers.Infra.Transactions;
using TodoUsers.Tests.Helpers;

namespace TodoUsers.Tests.Services
{
    [TestFixture]
    public class UserServices
    {
        private Mock<IUow> _uowMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private UserHelper _userHelper;

        [SetUp]
        public void Setup()
        {
            _uowMock = new Mock<IUow>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _userHelper = new UserHelper();
        }

        [Test]
        public void DeveRetornarUsuarioSeForEncontrado()
        {
            _userRepositoryMock.Setup(user => user.GetByUserName("vet")).Returns(_userHelper.UserValid);
            var sut = new UserService(_userRepositoryMock.Object, _uowMock.Object);

            var result = sut.GetByUserName("vet");

            Assert.IsNotNull(result);
        }

        [Test]
        public void DeveRetornarNullIfUsuarioNaoForEncontrado()
        {
            var sut = new UserService(_userRepositoryMock.Object, _uowMock.Object);

            var result = sut.GetByUserName("");

            Assert.IsNull(result);
        }

        [Test]
        public void DadoUsuarioValidoParametroValidDeveSerVerdadeiroAoAdicionar()
        {
            var sut = new UserService(_userRepositoryMock.Object, _uowMock.Object);

            var result = sut.Add(_userHelper.AddUserInputValid);

            Assert.IsTrue(result.Valid);
        }

        [Test]
        public void DadoUsuarioInvalidoParametroInvalidDeveSerVerdadeiroAoAdicionar()
        {
            var sut = new UserService(_userRepositoryMock.Object, _uowMock.Object);

            var result = sut.Add(_userHelper.AddUserInputInvalid);

            Assert.IsTrue(result.Invalid);
        }

        [Test]
        public void DeveRetornarNullIfUsuarioNaoForEncontradoAoEditar()
        {
            var sut = new UserService(_userRepositoryMock.Object, _uowMock.Object);

            var result = sut.Update(_userHelper.UpdateUserInputInvalid);

            Assert.IsNull(result);
        }

        [Test]
        public void DadoUsuarioValidoParametroValidDeveSerVerdadeiroAoEditar()
        {
            _userRepositoryMock.Setup(user => user.GetByUserName("vet")).Returns(_userHelper.UserValid);
            var sut = new UserService(_userRepositoryMock.Object, _uowMock.Object);

            var result = sut.Update(_userHelper.UpdateUserInputValid);

            Assert.IsTrue(result.Valid);
        }

        [Test]
        public void DadoUsuarioInvalidoParametroValidDeveSerVerdadeiroAoEditar()
        {
            _userRepositoryMock.Setup(user => user.GetByUserName("vet")).Returns(_userHelper.UserInvalid);
            var sut = new UserService(_userRepositoryMock.Object, _uowMock.Object);

            var result = sut.Update(_userHelper.UpdateUserInputValid);

            Assert.IsTrue(result.Invalid);
        }

        [Test]
        public void DeveRetornarNullIfUsuarioNaoForEncontradoAoRemover()
        {
            var sut = new UserService(_userRepositoryMock.Object, _uowMock.Object);

            var result = sut.Remove("");

            Assert.IsNull(result);
        }

        [Test]
        public void DeveRetornarUsuarioAoRemover()
        {
            _userRepositoryMock.Setup(user => user.GetByUserName("vet")).Returns(_userHelper.UserValid);
            var sut = new UserService(_userRepositoryMock.Object, _uowMock.Object);

            var result = sut.Remove("vet");

            Assert.IsNotNull(result);
        }
    }
}
