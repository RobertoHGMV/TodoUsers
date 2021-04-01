namespace TodoUsers.Domain.ValueObjects.NameObj
{
    public class Name
    {
        protected Name() { }

        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            Validator = new NameValidator();
        }

        public NameValidator Validator { get; set; }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public override string ToString() => $"{FirstName} {LastName}";
    }
}
