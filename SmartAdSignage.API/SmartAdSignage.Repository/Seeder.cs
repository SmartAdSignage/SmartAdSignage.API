using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartAdSignage.Core.Models;
using SmartAdSignage.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Repository
{
    public class Seeder : ISeeder
    {
        public Seeder()
        {
        }

        public async Task EnsureSeedDataAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var aspNetCoreIdentityDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await aspNetCoreIdentityDbContext.Database.MigrateAsync();
            await SeedAsync(serviceProvider);
        }

        private async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var countUsers = _userManager.Users.Count();

            if (countUsers == 0)
            {
                List<string> userNames = new() { "user@gmail.com", "admin@gmail.com" };

                foreach (var username in userNames)
                {
                    if (await _roleManager.FindByNameAsync("User") == null)
                        await _roleManager.CreateAsync(new IdentityRole("User"));
                    else
                        await _roleManager.CreateAsync(new IdentityRole("Admin"));

                    var user = await _userManager.FindByNameAsync(username);

                    if (user != null)
                    {
                        continue;
                    }

                    user = new Faker<User>()
                        .RuleFor(u => u.UserName, username)
                        .RuleFor(u => u.FirstName, f => f.Person.FirstName)
                        .RuleFor(u => u.LastName, f => f.Person.LastName)
                        .RuleFor(u => u.CompanyName, f => f.Company.CompanyName())
                        .RuleFor(u => u.Email, username)
                        .RuleFor(u => u.EmailConfirmed, true).Generate();
                    var result = await _userManager.CreateAsync(user, "Pass123$");

                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    if (user.Email.Contains("user"))
                        result = await _userManager.AddToRoleAsync(user, "User");
                    else
                        result = await _userManager.AddToRoleAsync(user, "Admin");

                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    /*result = await _userManager.AddClaimsAsync(
                        user,
                        new Claim[]
                        {
                            new Claim(JwtClaimTypes.GivenName, user.FirstName),
                            new Claim(JwtClaimTypes.FamilyName, user.LastName),
                            new Claim(JwtClaimTypes.Email, user.Email)
                        });*/

                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                }
            }
        }
    }
}
