using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;

namespace Core.Aspects.AspectCore.Caching.MemoryCache
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class MemoryCacheAspect : AbstractInterceptorAttribute
    {
        private readonly int _duration;
        private readonly string _prefix;
        
        public MemoryCacheAspect(int duration, string prefix = "")
        {
            _duration = duration;
            _prefix = prefix;
        }

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            var cacheService = (ICacheService)context.ServiceProvider.GetService(typeof(ICacheService));
            if (cacheService == null)
            {
                await next(context);
                return;
            }
            var methodName = context.ImplementationMethod.Name;
            var arguments = context.Parameters.Select(a => a?.ToString() ?? "null").ToArray();
            var key = $"{_prefix}";
            
            if (await cacheService.ExistsAsync(key))
            {
                context.ReturnValue = await cacheService.GetAsync<object>(key);
                return;
            }
            
            await next(context);
            await cacheService.AddAsync(key, context.ReturnValue, TimeSpan.FromMinutes(_duration));
            var xx =  cacheService.GetAsync<object>(key).Result;
        }
    }
}