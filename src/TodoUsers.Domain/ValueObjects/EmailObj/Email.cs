namespace TodoUsers.Domain.ValueObjects.EmailObj
{
    public class Email
    {
        protected Email() { }

        public Email(string address)
        {
            Address = address;
        }

        public string Address { get; private set; }
    }
}
