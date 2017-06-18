using System;
using System.Collections;
using System.Collections.Generic;

namespace DataManagement.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get();
        T Get(int id);
        void Add(T entity);
        void Delete(int id);
        void Update(T entity);

        IEnumerable<T> GetTransactionsForDateRange(DateTime start, DateTime end, int? idPortfolio,
            int? idPortfolioList);

        IEnumerable<T> Get(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList);
    }
}