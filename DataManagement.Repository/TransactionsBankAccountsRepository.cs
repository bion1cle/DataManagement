using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DataManagement.Entities;
using DataManagement.Repository.Interfaces;

namespace DataManagement.Repository
{
    public class TransactionsBankAccountsDTGRepository : BaseRepository, IRepository<TransactionsBankAccountsDTG>
    {
        public IEnumerable<TransactionsBankAccountsDTG> Get()
        {
            throw new NotImplementedException();
        }

        public TransactionsBankAccountsDTG Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(TransactionsBankAccountsDTG entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(TransactionsBankAccountsDTG entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TransactionsBankAccountsDTG> GetTransactionsForDateRange(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            //DynamicParameters parameters = new DynamicParameters();
            //parameters.Add("@startDate", start, System.Data.DbType.Date);
            //parameters.Add("@endDate", end, System.Data.DbType.Date);
            //parameters.Add("@idPortfolio", idPortfolio.HasValue ? idPortfolio : null, System.Data.DbType.Int32);
            //parameters.Add("@IdPortfolioList", idPortfolioList.HasValue ? idPortfolioList : null, System.Data.DbType.Int32);

            //IList<TransactionsBankAccountsDTG> transactionList = SqlMapper.Query<TransactionsBankAccountsDTG>(con, "RIED.uspPerformanceContributionGetTransactionsBankAccounts", param: parameters,
            //    commandType: System.Data.CommandType.StoredProcedure).ToList();
            //return transactionList;
            throw new NotImplementedException();
        }

        public IEnumerable<TransactionsBankAccountsDTG> Get(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            throw new NotImplementedException();
        }
    }
}