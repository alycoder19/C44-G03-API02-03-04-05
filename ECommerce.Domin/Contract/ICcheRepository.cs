using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domin.Contract
{
    public interface ICcheRepository
    {

        Task<string?> GetAsync(string CacheKey);
        Task SetAsync(string CacheKey,string CacheValue ,TimeSpan TimeToLive);


    }
}
