using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QS.Authentication.Server.Data.Context;
using QS.Authentication.Server.Data.Models;
using QS.Authentication.Server.Data.Seeders;
using System.Reflection;

namespace QS.Authentication.Server.Data
{
    public static class DIExtensions
    {
        public static void RegisterDataServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<UsersDbContext>(options =>
                options.UseSqlServer(connectionString, x => x.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name)));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<UsersDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<UsersSeeder>();
        }

        public static IApplicationBuilder PrepareDb(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var serviceProvider = serviceScope.ServiceProvider;

            MigrateDatabase(serviceProvider);
            SeedUsers(serviceProvider);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<UsersDbContext>();
            context.Database.Migrate();
        }

        private static void SeedUsers(IServiceProvider serviceProvider)
        {
            var seeder = serviceProvider.GetRequiredService<UsersSeeder>();
            seeder.SeedData();
        }
    }
}
