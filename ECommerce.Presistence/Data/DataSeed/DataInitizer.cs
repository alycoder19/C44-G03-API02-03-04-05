using ECommerce.Domin.Contract;
using ECommerce.Domin.Entity;
using ECommerce.Domin.Entity.ProductModule;
using ECommerce.Presistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Presistence.Data.DataSeed
{
    public class DataInitizer : IDatalnitlizer
    {
        private readonly StoreDbContext _dbcontext;

        public DataInitizer(StoreDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task InitilizeAsync()
        {
            try
            {
                var HasProducts = await _dbcontext.Products.AnyAsync();
                var HasBrands = await _dbcontext.ProductBrands.AnyAsync();
                var HasTypes = await _dbcontext.ProductTypes.AnyAsync();
                if (HasProducts && HasBrands && HasTypes) return;

                if (!HasBrands)
                await  SeedDataFromJesonAsync<ProductBrand, int>("brands.json",_dbcontext.ProductBrands);
             
               if(!HasTypes)
                  await  SeedDataFromJesonAsync<ProductType, int>("types.json", _dbcontext.ProductTypes);
               _dbcontext.SaveChanges();
                if (!HasProducts)
                   await SeedDataFromJesonAsync<Product, int>("Products.json", _dbcontext.Products);
                _dbcontext.SaveChanges();





            }
            catch (Exception ex)
            {
                Console.WriteLine($"Data Seeding Failed : {ex}");
               
            }
        }

        private async Task SeedDataFromJesonAsync<T, Tkey>(string fileName, DbSet<T> dbset) where T : BaseEntity<Tkey>
        {
            //Filepath
            //G:\courses\.net\.Net API\ECommerce.Presistence\Data\DataSeed\JSONFiles\

            var FilePath = @"..\ECommerce.Presistence\Data\DataSeed\JSONFiles\"+fileName;

            if (!File.Exists(FilePath)) throw new FileNotFoundException($"File {fileName} Not Found !");

            try
            {
                using var DataStream = File.OpenRead(FilePath);
                var Data = JsonSerializer.Deserialize<List<T>>(DataStream , new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive=true,


                });
                if (Data is not null)
                {
                    

                 await dbset.AddRangeAsync(Data);
                
                }

                
            }
            catch (Exception ex )
            {

                Console.WriteLine($"Filed to Read Data From JSON : {ex}");
            }




        }

    }
}
