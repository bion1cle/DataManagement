﻿using System;
using System.Collections.Generic;
using DataManagement.Business.Interfaces;
using DataManagement.Entities;
using DataManagement.Repository.Interfaces;

namespace DataManagement.Business
{
    public class TransactionsBankAccountsManager : BaseManager<TransactionsBankAccounts>, IManager<TransactionsBankAccounts>
    {
        public TransactionsBankAccountsManager(IRepository<TransactionsBankAccounts> repository) : base(repository)
        {
        }

        public void LoadData()
        {
            throw new NotImplementedException();
        }

        public void LoadData(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            this.Data = base.Repository.GetTransactionsForDateRange(start, end, idPortfolio, idPortfolioList);
        }
        

        public IEnumerable<TransactionsBankAccounts> Data { get; set; }
    }
}