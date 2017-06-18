using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DataManagement.Entities;

using DataManagement.Repository.Interfaces;

namespace DataManagement.Repository
{
    public class TransactionRepository : BaseRepository, IRepository<Transaction>
    {
        public IEnumerable<Transaction> Get()
        {
            throw new System.NotImplementedException();
        }

        public Transaction Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Add(Transaction entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Transaction entity)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Transaction> GetTransactionsForDateRange(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@startDate", start, System.Data.DbType.Date);
            parameters.Add("@endDate", end, System.Data.DbType.Date);
            parameters.Add("@idPortfolio", idPortfolio.HasValue ? idPortfolio:null, System.Data.DbType.Int32);
            parameters.Add("@IdPortfolioList", idPortfolioList.HasValue ? idPortfolioList : null , System.Data.DbType.Int32);

            IList<Transaction> transactionList = SqlMapper.Query<Transaction>(con, "RIED.uspPerformanceContributionGetTransactions", param: parameters,
                commandType: System.Data.CommandType.StoredProcedure).ToList();
            return transactionList;
        }

        public IEnumerable<Transaction> Get(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            throw new NotImplementedException();
        }
    }
}