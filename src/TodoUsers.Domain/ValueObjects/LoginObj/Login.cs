namespace TodoUsers.Domain.ValueObjects.LoginObj
{
    public class Login
    {
        protected Login() { }

        public Login(string userName, string password, string confirmPassoword)
        {
            UserName = userName.ToLower().Trim();
            Password = password.Trim();
            ConfirmPassword = confirmPassoword.Trim();
            Validator = new LoginValidator();
        }

        public LoginValidator Validator { get; set; }

        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string ConfirmPassword { get; private set; }

        public bool Authenticate(string userName, string password)
        {
            return UserName.Equals(userName) && Password.Equals(password);
        }
    }
}
