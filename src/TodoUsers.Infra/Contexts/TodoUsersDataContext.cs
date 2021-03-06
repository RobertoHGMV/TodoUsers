using Microsoft.EntityFrameworkCore;
using TodoUsers.Common;
using TodoUsers.Domain.Models.Users;
using TodoUsers.Domain.ValueObjects.EmailObj;
using TodoUsers.Domain.ValueObjects.LoginObj;
using TodoUsers.Domain.ValueObjects.NameObj;
using TodoUsers.Infra.Mappings;

namespace TodoUsers.Infra.Contexts
{
    public class TodoUsersDataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseInMemoryDatabase("Database");
                optionsBuilder.UseSqlServer(Runtime.ConnectionStringSqlServer);
            }
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Ignore<Name>();
            builder.Ignore<Login>();
            builder.Ignore<Email>();

            builder.ApplyConfiguration(new UserMap());
        }
    }
}
