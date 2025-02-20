using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces.Entities;
using Core.Interfaces.Repositories;

namespace Core.Interfaces.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        IGenericRepository<T> GetRepository<T>() where T : class,IEntity,new();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        void BeginTransaction();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();

        void DisposeTransaction();
        Task DisposeTransactionAsync();

    }
}