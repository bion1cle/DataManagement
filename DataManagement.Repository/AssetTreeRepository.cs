using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DataManagement.Entities;
using DataManagement.Repository.Interfaces;

namespace DataManagement.Repository
{
    public class AssetTreeRepository : BaseRepository, IRepository<AssetTree> 
    {
        public IEnumerable<AssetTree> Get()
        {
            IList<AssetTree> assetTree = SqlMapper.Query<AssetTree>(con, "SELECT VATCA.IdAssetClassChild\r\n\t\t,VATCA.Level\r\n\t\t,VATCA.AssetClassParent\r\n\t\t,VATCA.AssetClassChild\r\n\t\t,VATCA.AssetClassChildName\r\n\t\t,VATCA.IdAssetClassParent\r\n\t\t,VATCA.LocalId\r\n\t\t,VATCA.AssetClassSetID FROM dbo.vAssetTreeContributionApp AS VATCA", commandType: System.Data.CommandType.Text).ToList();
            return assetTree;
        }

        public AssetTree Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(AssetTree entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(AssetTree entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AssetTree> GetTransactionsForDateRange(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AssetTree> Get(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            throw new NotImplementedException();
        }
    }
}