using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Utilities.Results.Abstracts;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICustomerService
    {
        Task<IDataResult<IEnumerable<Customer>>> GetAllAsync();
        Task<IResult> AddAsync(Customer customer);
    }
}