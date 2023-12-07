using SmartAdSignage.Core.Extra;
using SmartAdSignage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Repository.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> AddAsync(TEntity entity);
        TEntity Update(TEntity entity);
        bool Delete(TEntity entity);
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddManyAsync(IEnumerable<TEntity> obj);

        Task<IList<TEntity>> GetByConditionAsync(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TEntity>> selector = null);

        Task<IList<TEntity>> GetPageWithMultiplePredicatesAsync(
        IEnumerable<Expression<Func<TEntity, bool>>> predicates,
        PageInfo pageInfo,
        Expression<Func<TEntity, TEntity>> selector);

        Task SaveAsync();
    }
}
