using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheService
    {
        Task AddAsync(string key, object value, TimeSpan? expiry = null);
        Task<T> GetAsync<T>(string key);
        Task RemoveAsync(string key);
        Task<bool> ExistsAsync(string key);
        Task AddOrUpdateAsync(string key, object value, TimeSpan? expiry = null);
        Task AddPatternAsync(string pattern, object value, TimeSpan? expiry = null);
        Task RemovePatternAsync(string pattern);
    }
}