using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;

namespace Core.Aspects.AspectCore.Exception
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Class)]
    public class ExceptionHandlingAspect:AbstractInterceptorAttribute
    {
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            try
            {
                // Metodu çalıştır
                await next(context);
            }
            catch (System.Exception ex)
            {
                // Hata yakalandığında yapılacak işlemler
                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
                // Hata loglama, kullanıcıya bilgi verme gibi işlemler yapılabilir
            }
        }
    }
}