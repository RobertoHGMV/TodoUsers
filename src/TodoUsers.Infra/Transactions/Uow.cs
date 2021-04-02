using TodoUsers.Infra.Contexts;

namespace TodoUsers.Infra.Transactions
{
    public class Uow : IUow
    {
        TodoUsersDataContext _context;

        public Uow(TodoUsersDataContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Rollback() { }
    }
}
