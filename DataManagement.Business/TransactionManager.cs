using System;
using System.Collections.Generic;
using DataManagement.Business.Interfaces;
using DataManagement.Entities;
using DataManagement.Repository.Interfaces;

namespace DataManagement.Business
{
    public class TransactionManager : BaseManager<Transaction>, IManager<Transaction>
    {
        private readonly IRepository<Transaction> _transactionRepository;
        public TransactionManager(IRepository<Transaction> transactionRepository):base(transactionRepository)
        {
            //base
            //_transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
        }
        public void LoadData()
        {
            _transactionRepository.Get();
        }

        public void LoadData(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            this.Data = base.Repository.GetTransactionsForDateRange(start, end, idPortfolio, idPortfolioList);
        }
        

        public IEnumerable<Transaction> Data { get; set; }

       
    }
}