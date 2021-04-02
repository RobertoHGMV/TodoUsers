using System.Collections.Generic;
using TodoUsers.Domain.Models.Users;

namespace TodoUsers.Domain.Repositories
{
    public interface IUserRepository
    {
        IList<User> GetAll();
        User GetByUserName(string userName);
        void Add(User user);
        void Update(User user);
        void Remove(User user);
    }
}
