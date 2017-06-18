using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DataManagement.Entities;
using DataManagement.Repository.Interfaces;

namespace DataManagement.Repository
{
    public class PortfolioRepository : BaseRepository, IRepository<Portfolio>
    {
        public IEnumerable<Portfolio> Get()
        {
            IList<Portfolio> portfolioList = SqlMapper.Query<Portfolio>(con, "SELECT P.IdPortfolio, P.PortfolioShortName, P.PortfolioName,P.Manager\r\n\t\t\t\t\tFROM CCO.Portfolio AS P\r\n\t\t\t\t\tINNER JOIN CCO.PortfolioGroup AS PG ON PG.IdPortfolioGroup = P.IdPortfolioGroup\r\n\t\t\t\t\tINNER JOIN CCO.PortfolioGroupType AS PGT ON PGT.IdPortfolioGroupType = PG.IdPortfolioGroupType\r\n\t\t\t\t\tWHERE P.IdDataSource = 1 AND P.InactiveFlag = 0 AND PGT.PortfolioGroupTypeShortName = \'Kunde\'\r\n\t\t\t\t\tAND P.PortfolioShortName <> \'LABS\'", commandType: System.Data.CommandType.Text).ToList();
            return portfolioList;
        }

        public Portfolio Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Add(Portfolio entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Portfolio entity)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Portfolio> GetTransactionsForDateRange(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Portfolio> Get(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            throw new NotImplementedException();
        }
    }
}