﻿using System;
using System.Collections.Generic;
using DataManagement.Business.Interfaces;
using DataManagement.Entities;
using DataManagement.Repository.Interfaces;

namespace DataManagement.Business
{
    public class TransactionManager : ManagerBase<Transaction>, IManager<Transaction>
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

        public void LoadData(DateTime start, DateTime end, int idPortfolio)
        {
            //this.Data = _transactionRepository.GetTransactionsForDateRange(start, end, idPortfolio, null);
            this.Data = base.Repository.GetTransactionsForDateRange(start, end, idPortfolio, null);
            //ManagerBase<Transaction>,
        }

        public IEnumerable<Transaction> Data { get; set; }

       
    }
}