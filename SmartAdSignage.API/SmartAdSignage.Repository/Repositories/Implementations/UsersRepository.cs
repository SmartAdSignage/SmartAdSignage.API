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
    public class UsersRepository : IUsersRepository
    {
        public ApplicationDbContext Context { get; set; }
        public UserManager<User> _userManager { get; set; }
        public UsersRepository(ApplicationDbContext context, UserManager<User> userManager)
        {
            this.Context = context;
            this._userManager = userManager;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await Context.Users.ToListAsync();
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }
    }
}
