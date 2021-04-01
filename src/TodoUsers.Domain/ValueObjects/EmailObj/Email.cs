namespace TodoUsers.Domain.ValueObjects.EmailObj
{
    public class Email
    {
        protected Email() { }

        public Email(string address)
        {
            Address = address;
            Validator = new EmailValidator();
        }

        public EmailValidator Validator { get; set; }

        public string Address { get; private set; }
    }
}
