using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces.Repositories;
using Domain.Entities;

namespace Infrastructure.Interfaces
{
    public interface ICustomerRepository:IGenericRepository<Customer>
    {
        
    }
}