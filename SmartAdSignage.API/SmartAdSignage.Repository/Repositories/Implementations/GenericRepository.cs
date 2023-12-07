using Microsoft.EntityFrameworkCore;
using SmartAdSignage.Core.Extra;
using SmartAdSignage.Core.Models;
using SmartAdSignage.Repository.Data;
using SmartAdSignage.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Repository.Repositories.Implementations
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly ApplicationDbContext _applicationDbContext;

        public GenericRepository(ApplicationDbContext applicationDbContext)
        {
            this._dbSet = applicationDbContext.Set<TEntity>();
            this._applicationDbContext = applicationDbContext;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var result = (await _dbSet.AddAsync(entity)).Entity;
            return result;
        }

        public bool Delete(TEntity entity)
        {
            try
            {
                _dbSet.Remove(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var entities = await _dbSet.ToListAsync();
            return entities;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IList<TEntity>> GetPageWithMultiplePredicatesAsync(
        IEnumerable<Expression<Func<TEntity, bool>>> predicates,
        PageInfo pageInfo,
        Expression<Func<TEntity, TEntity>> selector)
        {
            var skip = pageInfo.Size * (pageInfo.Number - 1);

            IQueryable<TEntity> query = _dbSet.AsQueryable();

            if (selector != null)
            {
                query = query.Select(selector);
            }

            if (predicates == null || predicates.Count() == 0)
            {
                query = query.Skip(skip).Take(pageInfo.Size);
            }
            else
            {
                foreach (var predicate in predicates)
                {
                    query = query.Where(predicate).Skip(skip).Take(pageInfo.Size);
                }
            }

            var entities = await query.ToListAsync();
            return entities;
        }

        public async Task<IList<TEntity>> GetByConditionAsync(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TEntity>> selector = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (selector != null)
            {
                query = query
                    .Where(predicate)
                    .Select(selector);
            }
            else
            {
                query = query.Where(predicate);
            }

            return await query.ToListAsync();
        }

        public TEntity Update(TEntity entity)
        {
            return _dbSet.Update(entity).Entity;
        }

        /// <inheritdoc />
        public async Task AddManyAsync(IEnumerable<TEntity> obj)
        {
            await _dbSet.AddRangeAsync(obj);
        }

        public async Task SaveAsync()
        {
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
