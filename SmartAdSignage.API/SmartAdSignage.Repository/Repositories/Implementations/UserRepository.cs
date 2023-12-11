using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartAdSignage.Core.Models;
using SmartAdSignage.Repository.Data;
using SmartAdSignage.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Repository.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        public ApplicationDbContext Context { get; set; }
        public UserManager<User> userManager { get; set; }
        public UserRepository(ApplicationDbContext context, UserManager<User> userManager)
        {
            this.Context = context;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await Context.Users.ToListAsync();
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await userManager.FindByNameAsync(username);
        }

        public async Task<bool> SaveRefreshToken(string username, string refreshToken)
        {
            var user = await Context.Users.SingleOrDefaultAsync(u => u.UserName == username);
            user.RefreshToken = refreshToken;
            return 0 < await Context.SaveChangesAsync();
        }

        public async Task<IdentityResult> UpdateUser(User user)
        {
            return await userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> CreateUserAsync(User user, string password)
        {
            return await userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> AddRoleToUserAsync(User user, string role)
        {
            return await userManager.AddToRoleAsync(user, role);
        }

        public async Task<IList<string>> GetRolesForUserAsync(User user)
        {
            return await userManager.GetRolesAsync(user);
        }

        public async Task<bool> CheckPasswordForUserAsync(User user, string password)
        {
            return await userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IdentityResult> DeleteUserAsync(User user)
        {
            return await userManager.DeleteAsync(user);
        }
    }
}
