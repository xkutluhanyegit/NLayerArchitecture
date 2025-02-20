using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Services;
using Application.Validation.FluentValidation;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.MemoryCacheService;
using Core.Interfaces.Repositories;
using Core.Interfaces.UnitOfWork;
using FluentValidation.AspNetCore;
using Infrastructure.Contexts;
using Infrastructure.Interfaces;
using Infrastructure.Persistence.Repositories;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection.AspectCore
{
    public static class DependencyInjectionModule
    {
        public static IServiceCollection AddAspectCoreServices(this IServiceCollection services)
        {
            //Services
            services.AddScoped<ICustomerService, CustomerService>();

            //Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Context
            services.AddScoped<NorthwindContext>();

            //Fluent Validation Dependencies 
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CustomerValidator>());

            //Cache
            services.AddMemoryCache();
            services.AddSingleton<ICacheService, MemoryCacheService>();


            


            return services;
        }
        
    }
}