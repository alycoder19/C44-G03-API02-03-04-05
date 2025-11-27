using ECommerce.Domin.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domin.Contract
{
    public interface IGenericRepository<TEntity , TKey> where TEntity : BaseEntity<TKey>
    {

        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity, TKey> specifiction);
        Task<TEntity?> GetByIdAsync(int id);
        Task<TEntity?> GetByIdAsync(ISpecification<TEntity, TKey>specifiction);
        Task AddAsync(TEntity entity);

        void Update(TEntity entity);
        void Remove(TEntity entity);
        Task<int> CountAsync(ISpecification<TEntity,TKey>specification);
         



    }
}
