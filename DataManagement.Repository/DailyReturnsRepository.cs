using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DataManagement.Entities;
using DataManagement.Repository.Interfaces;

namespace DataManagement.Repository
{
    public class DailyReturnsRepository : BaseRepository, IRepository<DailyReturns>
    {
        public IEnumerable<DailyReturns> Get()
        {
            throw new NotImplementedException();
        }

        public DailyReturns Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(DailyReturns entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(DailyReturns entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DailyReturns> GetTransactionsForDateRange(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DailyReturns> Get(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@startDate", start, System.Data.DbType.Date);
            parameters.Add("@endDate", end, System.Data.DbType.Date);
            parameters.Add("@idPortfolio", idPortfolio.HasValue ? idPortfolio : null, System.Data.DbType.Int32);
            parameters.Add("@IdPortfolioList", idPortfolioList.HasValue ? idPortfolioList : null, System.Data.DbType.Int32);

            IList<DailyReturns> transactionList = SqlMapper.Query<DailyReturns>(con, "RIED.ssss", param: parameters,
                commandType: System.Data.CommandType.StoredProcedure).ToList();
            return transactionList;
        }
    }
}