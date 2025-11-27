using ECommerce.Domin.Contract;
using ECommerce.Domin.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Presistence.Data.DbContexts;


namespace ECommerce.presentation.Repositories
{
    public class UnitOfwork : IUnitOfWork
    {
        private readonly StoreDbContext _dbContext;
        private readonly Dictionary<Type, object> _repositories = [];

        public UnitOfwork(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
           var EntityType= typeof(TEntity);
            if (_repositories.TryGetValue(EntityType,out var repository)) 
            {

                return (IGenericRepository<TEntity,TKey>) repository;
            }

            var NewRepo=new GenericRepositories<TEntity ,  TKey>(_dbContext);
            _repositories[EntityType] = NewRepo;
            return NewRepo;

        }

        public async Task<int> SaveChangesAysnc()
             => await _dbContext.SaveChangesAsync();
      

    }
}
