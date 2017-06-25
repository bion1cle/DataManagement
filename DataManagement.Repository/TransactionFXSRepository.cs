using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using DataManagement.Entities;
using DataManagement.Repository.Interfaces;

namespace DataManagement.Repository
{
    public class TransactionFXSRepository : BaseRepository, IRepository<TransactionFXS>
    {
        public IEnumerable<TransactionFXS> Get()
        {
            throw new NotImplementedException();
        }

        public TransactionFXS Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(TransactionFXS entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(TransactionFXS entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TransactionFXS> GetTransactionsForDateRange(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TransactionFXS> Get(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@startDate", start, System.Data.DbType.Date);
            parameters.Add("@endDate", end, System.Data.DbType.Date);
            parameters.Add("@idPortfolio", idPortfolio.HasValue ? idPortfolio : null, System.Data.DbType.Int32);
            parameters.Add("@IdPortfolioList", idPortfolioList.HasValue ? idPortfolioList : null, System.Data.DbType.Int32);

            IList<TransactionFXS> transactionFXSList = SqlMapper.Query<TransactionFXS>(con, "RIED.uspPerformanceContributionGetTransactionsFXS", param: parameters,
                commandType: System.Data.CommandType.StoredProcedure).ToList();
            return transactionFXSList;
        }
    }
}
