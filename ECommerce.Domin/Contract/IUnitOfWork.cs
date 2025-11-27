using ECommerce.Domin.Entity;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domin.Contract
{
    public interface IUnitOfWork
    {

        Task<int> SaveChangesAysnc();
        IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;


    }
}
