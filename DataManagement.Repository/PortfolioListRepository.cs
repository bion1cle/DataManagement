using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DataManagement.Entities;
using DataManagement.Repository.Interfaces;

namespace DataManagement.Repository
{
    public class PortfolioListRepository : BaseRepository, IRepository<PortfolioList>
    {
        public IEnumerable<PortfolioList> Get()
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@IdPortfolioGroup", -3, System.Data.DbType.Int32);
            IList<PortfolioList> portfolioList = SqlMapper.Query<PortfolioList>(conIMDWH, "RIED.uspSSRSPortfolioList", param:parameter, commandType: System.Data.CommandType.StoredProcedure).ToList();
            return portfolioList;
        }

        public PortfolioList Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Add(PortfolioList entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(PortfolioList entity)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<PortfolioList> GetTransactionsForDateRange(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PortfolioList> Get(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            throw new NotImplementedException();
        }
    }
}