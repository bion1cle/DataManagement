using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DataManagement.Entities;
using DataManagement.Repository.Interfaces;

namespace DataManagement.Repository
{
    public class AssetClassificationRepository : BaseRepository, IRepository<AssetClassification>
    {
        public IEnumerable<AssetClassification> Get()
        {
            IList<AssetClassification> assetClassification = SqlMapper.Query<AssetClassification>(con, "SELECT AC.IdAssetClassification\r\n\t\t,AC.IdAssetClassificationParent\r\n\t\t,AC.IdAssetClassificationSet\r\n\t\t,AC.AssetClassificationShortName\r\n\t\t,AC.AssetClassificationName\r\n\t\t,AC.LocalAssetClassificationid\r\n\t\t,AC.IsDeleted FROM CCO.AssetClassification AS AC", commandType: System.Data.CommandType.Text).ToList();
            return assetClassification;
        }

        public AssetClassification Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(AssetClassification entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(AssetClassification entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AssetClassification> GetTransactionsForDateRange(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AssetClassification> Get(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            throw new NotImplementedException();
        }
    }
}