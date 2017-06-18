using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DataManagement.Entities;
using DataManagement.Repository.Interfaces;

namespace DataManagement.Repository
{
    public class InvestmentTypeRepository : BaseRepository, IRepository<InvestmentType>
    {
        public IEnumerable<InvestmentType> Get()
        {
            IList<InvestmentType> investmentTypeList = SqlMapper.Query<InvestmentType>(con, "SELECT \r\nVATC.AssetClassChild \r\nFROM dbo.vAssetTreeContribution AS VATC\r\nWHERE\tVATC.Level = 2\r\nAND VATC.AssetClassParent IN (\'Aktien\',\'Renten\',\'Liquidität\',\'Immobilien\',\'Währungsprodukte\',\'Rohstoffe\')\r\nORDER BY VATC.AssetClassParent", commandType: System.Data.CommandType.Text).ToList();
            return investmentTypeList;
        }

        public InvestmentType Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Add(InvestmentType entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(InvestmentType entity)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<InvestmentType> GetTransactionsForDateRange(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<InvestmentType> Get(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            throw new NotImplementedException();
        }
    }
}