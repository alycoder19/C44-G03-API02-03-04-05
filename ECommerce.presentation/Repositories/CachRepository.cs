using ECommerce.Domin.Contract;
using Microsoft.AspNetCore.Connections.Features;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.presentation.Repositories
{
    public class CachRepository : ICcheRepository
    {
        private readonly IDatabase _database;
        public CachRepository(IConnectionMultiplexer connection)
        {
            _database=connection.GetDatabase();
        }

        public async Task<string?> GetAsync(string CacheKey)
        {
           var cachValue=await _database.StringGetAsync(CacheKey);
            if(cachValue.IsNullOrEmpty)return null;
            return cachValue.ToString();
        }

        public async Task SetAsync(string CacheKey, string CacheValue, TimeSpan TimeToLive)
        {
            await _database.StringSetAsync(CacheKey, CacheValue, TimeToLive);
        }
    }
}
