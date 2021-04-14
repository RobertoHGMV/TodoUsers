using FluentMigrator.Runner;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TodoUsers.Common;
using TodoUsers.DependencyInjection;
using TodoUsers.FluentMigrations.DbMigrations;

namespace TodoUsers.Api.Rest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCompression();
            services.AddControllers()
                .AddNewtonsoftJson()
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            AddDocumentation(services);
            ResolveDependency(services);
            ConfigureFluentMigrator(services);
            Runtime.ConnectionStringSqlServer = Configuration["ConnStrSqlServer"];
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseResponseCompression();
            app.UseHttpsRedirection();
            
            app.UseRouting();
            app.UseCors(x => 
            x.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
            
            EnableDocumentation(app);

            //app.UseAuthentication();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            migrationRunner.MigrateUp();
        }

        private void ResolveDependency(IServiceCollection services)
        {
            new DependencyResolver().Resolver(services);
        }

        private void ConfigureFluentMigrator(IServiceCollection services)
        {
            services.AddFluentMigratorCore()
                .ConfigureRunner(
                builder => builder
                .WithVersionTable(new TableVersionMigration())
                .AddSqlServer()
                .WithGlobalConnectionString(Configuration["ConnStrSqlServer"])
                .ScanIn(typeof(TableVersionMigration).Assembly).For.Migrations());
        }

        #region

        private void AddDocumentation(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Lista de Usuários",
                    Version = "v1",
                    Description = "Cadastro de Usuários"
                });
            });
        }

        private void EnableDocumentation(IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lista de Usuários API V1");
                c.RoutePrefix = string.Empty;
            });
        }

        #endregion
    }
}
