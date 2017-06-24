using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using DataManagement.Entities;
using DataManagement.Repository.Interfaces;

namespace DataManagement.Repository
{
    public class PositionFactDataRepository : BaseRepository, IRepository<PositionFactData>
    {
        public IEnumerable<PositionFactData> Get()
        {
            throw new NotImplementedException();
            //uspPerformanceContributionGetPositionFact
        }

        public PositionFactData Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(PositionFactData entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(PositionFactData entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PositionFactData> GetTransactionsForDateRange(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PositionFactData> Get(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@startDate", start, System.Data.DbType.Date);
            parameters.Add("@endDate", end, System.Data.DbType.Date);
            

            IList<PositionFactData> positionFactDataList = SqlMapper.Query<PositionFactData>(con, "RIED.uspPerformanceContributionGetPositionFact", param: parameters,
                commandType: System.Data.CommandType.StoredProcedure).ToList();
            return positionFactDataList;
        }
    }
}
