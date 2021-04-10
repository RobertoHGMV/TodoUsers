using System;
using TodoUsers.Common.Entities;
using TodoUsers.Domain.ValueObjects.EmailObj;
using TodoUsers.Domain.ValueObjects.LoginObj;
using TodoUsers.Domain.ValueObjects.NameObj;

namespace TodoUsers.Domain.Models.Users
{
    public class User : Entity
    {
        public User(Name name, Login login, Email email)
        {
            Id = Guid.NewGuid();
            Name = name;
            Login = login;
            Email = email;

            Validate(this, new UserValidator());
        }

        protected User() { }

        public Guid Id { get; private set; }
        public Name Name { get; private set; }
        public Login Login { get; private set; }
        public Email Email { get; private set; }

        public void Update(Name name, Email email)
        {
            Name = name;
            Email = email;

            Validate(this, new UserValidator());
        }
    }
}
