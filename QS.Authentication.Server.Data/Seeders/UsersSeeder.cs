using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QS.Authentication.Server.Data.Context;
using QS.Authentication.Server.Data.Models;
using QS.Shared.Constants;
using System.Security.Claims;

namespace QS.Authentication.Server.Data.Seeders
{
    public class UsersSeeder
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<UsersSeeder> logger;

        public UsersSeeder(
            ILogger<UsersSeeder> logger, 
            IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }
        public void SeedData()
        {
            var context = this.serviceProvider.GetRequiredService<UsersDbContext>();

            var userManager = this.serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = this.serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (context.Users.Any())
            {
                this.logger.LogInformation("Users already seeded");
                return;
            }

            var adminRole = roleManager.FindByNameAsync(Role.Admin).Result;
            
            if(adminRole == null)
            {
                var role = new IdentityRole { Name = Role.Admin };

                _ = roleManager.CreateAsync(role).Result;

                this.logger.LogInformation("Administrator role created");
            }

            var userRole = roleManager.FindByNameAsync(Role.User).Result;

            if (userRole == null)
            {
                var role = new IdentityRole { Name = Role.User };

                _ = roleManager.CreateAsync(role).Result;

                this.logger.LogInformation("User role created");
            }
        

            var alice = new ApplicationUser
            {
                UserName = "alice",
                Email = "AliceSmith@email.com",
                EmailConfirmed = true
            };

            var createAlicewResult = userManager.CreateAsync(alice, "Pass123$").Result;
            if (!createAlicewResult.Succeeded)
            {
                throw new Exception(createAlicewResult.Errors.First().Description);
            }

            createAlicewResult = userManager.AddClaimsAsync(alice, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, "Alice Smith"),
                new Claim(JwtClaimTypes.GivenName, "Alice"),
                new Claim(JwtClaimTypes.FamilyName, "Smith"),
                new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                new Claim(JwtClaimTypes.Role, Role.Admin)
            }).Result;

            if (!createAlicewResult.Succeeded)
            {
                throw new Exception(createAlicewResult.Errors.First().Description);
            }

            this.logger.LogInformation("alice created");

            var bob = new ApplicationUser
            {
                UserName = "bob",
                Email = "BobSmith@email.com",
                EmailConfirmed = true
            };

            var createBobResult = userManager.CreateAsync(bob, "Pass123$").Result;
            if (!createBobResult.Succeeded)
            {
                throw new Exception(createBobResult.Errors.First().Description);
            }

            createBobResult = userManager.AddClaimsAsync(bob, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, "Bob Smith"),
                new Claim(JwtClaimTypes.GivenName, "Bob"),
                new Claim(JwtClaimTypes.FamilyName, "Smith"),
                new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                new Claim(JwtClaimTypes.Role, Role.User)
            }).Result;

            if (!createBobResult.Succeeded)
            {
                throw new Exception(createBobResult.Errors.First().Description);
            }

            this.logger.LogInformation("bob created");

            if(!userManager.IsInRoleAsync(alice, Role.Admin).Result)
            {
                _ = userManager.AddToRoleAsync(alice, Role.Admin).Result;
            }

            if (!userManager.IsInRoleAsync(bob, Role.User).Result)
            {
                _ = userManager.AddToRoleAsync(bob, Role.User).Result;
            }
        }
    }
}
