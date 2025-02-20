using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;

namespace Core.Aspects.AspectCore.Caching.MemoryCache
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class InvalidateMemoryCacheAspect : AbstractInterceptorAttribute
    {
        private readonly string _prefix;
    
        public InvalidateMemoryCacheAspect(string prefix = "")
        {
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
            
            // Veritabanı güncelleme, silme, ekleme vb. işlemlerinden sonra cache'in geçersiz kılınması sağlanacak.
            // Cache'i temizle veya güncelle (ekleme/güncelleme/silme işlemi sonrası)
            var xx =  cacheService.GetAsync<object>(key).Result;
            await cacheService.RemoveAsync(key);

            await next(context);  // Asıl metodun çalışmasını sağla
        }
    }
}