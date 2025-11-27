using ECommerce.Domin.Contract;
using ECommerce.Domin.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.presentation.Repositories;
using ECommerce.Presistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using ECommerce.Presistence;




namespace ECommerce.presentation.Repositories
{


    public class GenericRepositories<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _dbContext;

        public GenericRepositories(StoreDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }
        public async Task AddAsync(TEntity entity)

       => await _dbContext.Set<TEntity>().AddAsync(entity);


        public async Task<IEnumerable<TEntity>> GetAllAsync()
       => await _dbContext.Set<TEntity>().ToListAsync();

        

        public  async Task<TEntity?> GetByIdAsync(int id)

             => await _dbContext.Set<TEntity>().FindAsync(id);

        public void Remove(TEntity entity)
        =>  _dbContext.Set<TEntity>().Remove(entity);


        public void Update(TEntity entity)
     => _dbContext.Set<TEntity>().Update(entity);



        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity, TKey> specifiction)
        {

            var Query = SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specifiction);
            return await Query.ToListAsync();

        }

        public async Task<TEntity?> GetByIdAsync(ISpecification<TEntity, TKey> specifiction)
        {
            return await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specifiction).FirstOrDefaultAsync();

        }

        public async Task<int> CountAsync(ISpecification<TEntity, TKey> specification)
        {
            return await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(),specification).CountAsync();

        }
    }
}
