using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TodoUsers.Common;
using TodoUsers.Domain.Models.Users;
using TodoUsers.Domain.Repositories;
using TodoUsers.Domain.ValueObjects.EmailObj;
using TodoUsers.Domain.ValueObjects.LoginObj;
using TodoUsers.Domain.ValueObjects.NameObj;
using TodoUsers.Infra.Contexts;

namespace TodoUsers.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        TodoUsersDataContext _context;

        public UserRepository(TodoUsersDataContext context)
        {
            _context = context;
        }

        public IList<User> GetAll()
        {
            using (var conn = new SqlConnection(Runtime.ConnectionStringSqlServer))
            {
                var sb = new StringBuilder(@"SELECT [Id], [FirstName], [LastName], [UserName], [Password], [Address] FROM [User]");

                conn.Open();

                return conn.Query<User, Name, Login, Email, User>(sb.ToString(), (user, name, login, email) => 
                {
                    user = new User(name, login, email);
                    return user;
                }, splitOn: "FirstName, UserName, Address").ToList();
            }

            //return _context.Users.ToList();
        }

        public User GetByUserName(string userName)
        {
            return _context.Users.AsNoTracking().FirstOrDefault(c => c.Login.UserName == userName);
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
        }

        public void Update(User user)
        {
            _context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void Remove(User user)
        {
            _context.Users.Remove(user);
        }
    }
}
