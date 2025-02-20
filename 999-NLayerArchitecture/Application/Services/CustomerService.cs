
using Application.Interfaces;
using Application.Validation.FluentValidation;
using Core.Aspects.AspectCore.Caching.MemoryCache;
using Core.Aspects.AspectCore.Validation;
using Core.Interfaces.UnitOfWork;
using Core.Utilities.Results.Abstracts;
using Core.Utilities.Results.Concretes;
using Domain.Entities;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        IUnitOfWork _unitOfWork;
        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [ValidationAspect(typeof(CustomerValidator))]
        
        [InvalidateMemoryCacheAspect("customer_")]
        public async Task<IResult> AddAsync(Customer customer)
        {
            await _unitOfWork.GetRepository<Customer>().AddAsync(customer);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult();
        }

        [MemoryCacheAspect(60,"customer_")]
        public async Task<IDataResult<IEnumerable<Customer>>> GetAllAsync()
        {
            var result = await _unitOfWork.GetRepository<Customer>().GetAllAsync();
            return new SuccessDataResult<IEnumerable<Customer>>(result);
        }
    }
}