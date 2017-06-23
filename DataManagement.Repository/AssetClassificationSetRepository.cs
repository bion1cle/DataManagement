using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DataManagement.Entities;
using DataManagement.Repository.Interfaces;

namespace DataManagement.Repository
{
    public class AssetClassificationSetRepository : BaseRepository, IRepository<AssetClassificationSet>
    {
        public void Add(AssetClassificationSet entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AssetClassificationSet> Get()
        {
            IList<AssetClassificationSet> assetClassificationSet = SqlMapper.Query<AssetClassificationSet>(con, "SELECT ACS.IdAssetClassificationSet\r\n\t\t,ACS.IdAssetClassificationSetType\r\n\t\t,ACS.AssetClassificationSetName\r\n\t\t,ACS.AssetClassificationSetShortName\r\n\t\t,ACS.IsDeleted FROM CCO.AssetClassificationSet AS ACS", commandType: System.Data.CommandType.Text).ToList();
            return assetClassificationSet;
        }

        public AssetClassificationSet Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AssetClassificationSet> Get(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AssetClassificationSet> GetTransactionsForDateRange(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            throw new NotImplementedException();
        }

        public void Update(AssetClassificationSet entity)
        {
            throw new NotImplementedException();
        }
    }
}