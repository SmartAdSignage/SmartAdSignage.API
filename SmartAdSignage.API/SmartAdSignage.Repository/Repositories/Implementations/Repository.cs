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
    public class Repository : IRepository
    {
        public RepositoryContext Context { get; set; }
        public Repository(RepositoryContext context)
        {
            this.Context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await Context.Users.ToListAsync();
        }
    }
}
