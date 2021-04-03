using FluentMigrator.Runner.VersionTableInfo;
using Microsoft.Extensions.DependencyInjection;
using TodoUsers.Business.UserServices;
using TodoUsers.Domain.Repositories;
using TodoUsers.Domain.Services;
using TodoUsers.FluentMigrations.DbMigrations;
using TodoUsers.Infra.Contexts;
using TodoUsers.Infra.Repositories;
using TodoUsers.Infra.Transactions;

namespace TodoUsers.DependencyInjection
{
    public class DependencyResolver
    {
        public void Resolver(IServiceCollection services)
        {
            services.AddScoped<TodoUsersDataContext, TodoUsersDataContext>();
            services.AddTransient<IUow, Uow>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IVersionTableMetaData, TableVersionMigration>();
            //services.AddTransient<ITokenService, TokenService>();
        }
    }
}
