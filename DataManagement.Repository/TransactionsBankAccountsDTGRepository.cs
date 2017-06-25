using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DataManagement.Entities;
using DataManagement.Repository.Interfaces;

namespace DataManagement.Repository
{
    public class TransactionsBankAccountsRepository : BaseRepository, IRepository<TransactionsBankAccounts>
    {
        public IEnumerable<TransactionsBankAccounts> Get()
        {
            throw new NotImplementedException();
        }

        public TransactionsBankAccounts Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(TransactionsBankAccounts entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(TransactionsBankAccounts entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TransactionsBankAccounts> GetTransactionsForDateRange(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@startDate", start, System.Data.DbType.Date);
            parameters.Add("@endDate", end, System.Data.DbType.Date);
            parameters.Add("@idPortfolio", idPortfolio.HasValue ? idPortfolio : null, System.Data.DbType.Int32);
            parameters.Add("@IdPortfolioList", idPortfolioList.HasValue ? idPortfolioList : null, System.Data.DbType.Int32);

            IList<TransactionsBankAccounts> transactionList = SqlMapper.Query<TransactionsBankAccounts>(con, "RIED.uspPerformanceContributionGetTransactionsBankAccounts", param: parameters,
                commandType: System.Data.CommandType.StoredProcedure).ToList();
            return transactionList;
        }

        public IEnumerable<TransactionsBankAccounts> Get(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            throw new NotImplementedException();
        }
    }
}