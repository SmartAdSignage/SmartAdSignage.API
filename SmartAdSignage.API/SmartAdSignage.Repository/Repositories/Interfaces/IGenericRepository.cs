using SmartAdSignage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Repository.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> AddAsync(TEntity entity);
        TEntity UpdateAsync(TEntity entity);
        bool DeleteAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity obj, CancellationToken cancellationToken = default);
        Task AddManyAsync(IEnumerable<TEntity> obj, CancellationToken cancellationToken = default);

        Task Commit();
    }
}
