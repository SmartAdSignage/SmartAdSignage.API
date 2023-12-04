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
            try 
            {
                /*var result = _dbSet.Add(entity).Entity;*/
                var result = (await _dbSet.AddAsync(entity)).Entity;
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool DeleteAsync(TEntity entity)
        {
            try 
            {
                _dbSet.Remove(entity);
                return true;
            }
            catch(Exception)
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
        Expression<Func<TEntity, TEntity>> selector,
        CancellationToken cancellationToken = default)
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

            var entities = await query.ToListAsync(cancellationToken: cancellationToken);
            return entities;
        }

        public async Task<IList<TEntity>> GetByConditionAsync(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TEntity>> selector = null,
        CancellationToken cancellationToken = default)
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

            return await query.ToListAsync(cancellationToken: cancellationToken);
        }

        public TEntity UpdateAsync(TEntity entity)
        {
            return _dbSet.Update(entity).Entity;
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

        public async Task Commit()
        {
            await _applicationDbContext.SaveChangesAsync();
            /*try 
            {
                
            }
            catch (Exception)
            {
                throw;
            }*/
        }
    }
}
