using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using Microsoft.Extensions.Caching.Memory;

namespace Core.CrossCuttingConcerns.Caching.MemoryCacheService
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        public MemoryCacheService(IMemoryCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }
        public async Task AddAsync(string key, object value, TimeSpan? expiry = null)
        {
            if (value == null) return;
            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiry ?? TimeSpan.FromMinutes(60)
            };
            _cache.Set(key, value, options);
            await  Task.CompletedTask;
        }

        public async Task AddOrUpdateAsync(string key, object value, TimeSpan? expiry = null)
        {
            await RemoveAsync(key);
            await AddAsync(key, value, expiry);
        }

        public async Task AddPatternAsync(string pattern, object value, TimeSpan? expiry = null)
        {
            await AddAsync(pattern, value, expiry);
        }

        public async Task<bool> ExistsAsync(string key)
        {
            return await Task.FromResult(_cache.TryGetValue(key, out _));
        }

        public async Task<T> GetAsync<T>(string key)
        {
            return await Task.FromResult(_cache.TryGetValue(key, out var value) ? (T)value : default);
        }

        public async Task RemoveAsync(string key)
        {
            _cache.Remove(key);
            await  Task.CompletedTask;
        }

        public async Task RemovePatternAsync(string pattern)
        {
            var cacheEntries = _cache.GetType().GetField("_entries", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.GetValue(_cache) as IDictionary;
            if (cacheEntries == null) return;
            
            var keysToRemove = cacheEntries.Keys.Cast<object>()
                .Select(k => k.ToString())
                .Where(k => !string.IsNullOrEmpty(k) && Regex.IsMatch(k, pattern))
                .ToList();

            foreach (var key in keysToRemove)
            {
                await RemoveAsync(key);
            }
        }
    }
}