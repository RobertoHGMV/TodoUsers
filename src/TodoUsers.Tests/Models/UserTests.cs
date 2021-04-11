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
            var sut = new Name(nome, sobrenome);

            var result = new User(sut, _loginValid, _emailValid).Valid;

            Assert.IsFalse(result);
        }

        [Test]
        [TestCase("Sebastian", "")]
        [TestCase("Sebastian", "ab")]
        public void DadoSobrenomeInvalidoEntidadeDeveRetornarFalso(string nome, string sobrenome)
        {
            var sut = new Name(nome, sobrenome);

            var result = new User(sut, _loginValid, _emailValid).Valid;

            Assert.IsFalse(result);
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
            var sut = new Login(usuario, senha, confirmacaoDeSenha);

            var result = new User(_nameValid, sut, _emailValid).Valid;

            Assert.IsFalse(result);
        }

        [Test]
        [TestCase("")]
        [TestCase("abcd")]
        public void DadoEmailInvalidoEntidadeDeveRetornarFalso(string enderecoDeEmail)
        {
            var sut = new Email(enderecoDeEmail);

            var result = new User(_nameValid, _loginValid, sut).Valid;

            Assert.IsFalse(result);
        }

        [Test]
        public void DadoUsuarioValidoEntidadeDeveRetornarVerdadeiro()
        {
            var sut = new User(_nameValid, _loginValid, _emailValid);

            Assert.IsTrue(sut.Valid);
        }

        [Test]
        [TestCase("", "Vettel")]
        [TestCase("ab", "Vettel")]
        public void DadoPrimeiroNomeInvalidoEntidadeDeveRetornarFalsoAoAtualizar(string nome, string sobrenome)
        {
            var sut = new Name(nome, sobrenome);
            _userValid.Update(sut, _emailValid);

            Assert.IsFalse(_userValid.Valid);
        }

        [Test]
        [TestCase("Sebastian", "")]
        [TestCase("Sebastian", "ab")]
        public void DadoSobrenomeInvalidoEntidadeDeveRetornarFalsoAoAtualizar(string nome, string sobrenome)
        {
            var sut = new Name(nome, sobrenome);
            _userValid.Update(sut, _emailValid);

            Assert.IsFalse(_userValid.Valid);
        }

        [Test]
        [TestCase("")]
        [TestCase("abcd")]
        public void DadoEmailInvalidoEntidadeDeveRetornarFalsoAoAtualizar(string enderecoDeEmail)
        {
            var sut = new Email(enderecoDeEmail);
            _userValid.Update(_nameValid, sut);

            Assert.IsFalse(_userValid.Valid);
        }
    }
}
