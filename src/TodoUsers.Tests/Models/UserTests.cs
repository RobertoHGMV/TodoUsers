using NUnit.Framework;
using TodoUsers.Domain.Models.Users;
using TodoUsers.Domain.ValueObjects.EmailObj;
using TodoUsers.Domain.ValueObjects.LoginObj;
using TodoUsers.Domain.ValueObjects.NameObj;

namespace TodoUsers.Tests.Models
{
    [TestFixture]
    public class UserTests
    {
        private Name _nameValid;
        private Login _loginValid;
        private Email _emailValid;
        private User _userValid;

        [SetUp]
        public void Setup()
        {
            _nameValid = new Name("Sebastian", "Vetel");
            _loginValid = new Login("vet", "vet@123", "vet@123");
            _emailValid = new Email("vet@gmail.com");
            _userValid = new User(_nameValid, _loginValid, _emailValid);
        }

        [Test]
        [TestCase("", "Vettel")]
        [TestCase("ab", "Vettel")]
        public void DadoPrimeiroNomeInvalidoEntidadeDeveRetornarFalso(string nome, string sobrenome)
        {
            var name = new Name(nome, sobrenome);
            var user = new User(name, _loginValid, _emailValid);

            Assert.IsFalse(user.Valid);
        }

        [Test]
        [TestCase("Sebastian", "")]
        [TestCase("Sebastian", "ab")]
        public void DadoSobrenomeInvalidoEntidadeDeveRetornarFalso(string nome, string sobrenome)
        {
            var name = new Name(nome, sobrenome);
            var user = new User(name, _loginValid, _emailValid);

            Assert.IsFalse(user.Valid);
        }

        [Test]
        [TestCase("", "12345", "12345")]
        [TestCase("ab", "12345", "12345")]
        [TestCase("vet", "", "12345")]
        [TestCase("vet", "12", "12345")]
        [TestCase("vet", "12345", "123456")]
        [TestCase("vet", "123456", "12345")]
        public void DadoLoginInvalidoEntidadeDeveRetornarFalso(string usuario, string senha, string confirmacaoDeSenha)
        {
            var login = new Login(usuario, senha, confirmacaoDeSenha);
            var user = new User(_nameValid, login, _emailValid);

            Assert.IsFalse(user.Valid);
        }

        [Test]
        [TestCase("")]
        [TestCase("abcd")]
        public void DadoEmailInvalidoEntidadeDeveRetornarFalso(string enderecoDeEmail)
        {
            var email = new Email(enderecoDeEmail);
            var user = new User(_nameValid, _loginValid, email);

            Assert.IsFalse(user.Valid);
        }

        [Test]
        public void DadoUsuarioValidoEntidadeDeveRetornarVerdadeiro()
        {
            var user = new User(_nameValid, _loginValid, _emailValid);

            Assert.IsTrue(user.Valid);
        }

        [Test]
        [TestCase("", "Vettel")]
        [TestCase("ab", "Vettel")]
        public void DadoPrimeiroNomeInvalidoEntidadeDeveRetornarFalsoAoAtualizar(string nome, string sobrenome)
        {
            var name = new Name(nome, sobrenome);
            _userValid.Update(name, _emailValid);

            Assert.IsFalse(_userValid.Valid);
        }

        [Test]
        [TestCase("Sebastian", "")]
        [TestCase("Sebastian", "ab")]
        public void DadoSobrenomeInvalidoEntidadeDeveRetornarFalsoAoAtualizar(string nome, string sobrenome)
        {
            var name = new Name(nome, sobrenome);
            _userValid.Update(name, _emailValid);

            Assert.IsFalse(_userValid.Valid);
        }

        [Test]
        [TestCase("")]
        [TestCase("abcd")]
        public void DadoEmailInvalidoEntidadeDeveRetornarFalsoAoAtualizar(string enderecoDeEmail)
        {
            var email = new Email(enderecoDeEmail);
            _userValid.Update(_nameValid, email);

            Assert.IsFalse(_userValid.Valid);
        }
    }
}
