using System;
using DataManagement.Repository.Interfaces;

namespace DataManagement.Business.Interfaces
{
    public abstract class ManagerBase<T> where T : class
    {
        protected ManagerBase(IRepository<T> repository)
        {
            //_transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
            Repository = repository ?? throw new ArgumentNullException(nameof(T));
        }

        protected IRepository<T> Repository { get; }
    }
}