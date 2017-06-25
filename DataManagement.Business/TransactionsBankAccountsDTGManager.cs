using System;
using System.Collections.Generic;
using System.Text;
using DataManagement.Business.Interfaces;
using DataManagement.Entities;
using DataManagement.Repository.Interfaces;

namespace DataManagement.Business
{
    public class TransactionsBankAccountsDTGManager : BaseManager<TransactionsBankAccountsDTG>, IManager<TransactionsBankAccountsDTG>
    {
        public TransactionsBankAccountsDTGManager(IRepository<TransactionsBankAccountsDTG> repository) : base(repository)
        {
        }

        public void LoadData()
        {
            throw new NotImplementedException();
        }

        public void LoadData(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TransactionsBankAccountsDTG> Data { get; set; }
    }
}
