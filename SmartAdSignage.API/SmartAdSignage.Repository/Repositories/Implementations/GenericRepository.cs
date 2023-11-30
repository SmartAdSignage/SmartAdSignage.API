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
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(ApplicationDbContext applicationDbContext)
        {
            this._dbSet = applicationDbContext.Set<TEntity>();
        }
        public Task<TEntity> AddAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var entities = await _dbSet.ToListAsync();
            return entities;
        }

        public Task<TEntity> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> UpdateAsyn(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(TEntity obj, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(obj, cancellationToken: cancellationToken);
        }

        /// <inheritdoc />
        public async Task AddManyAsync(IEnumerable<TEntity> obj, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(obj, cancellationToken: cancellationToken);
        }
    }
}
