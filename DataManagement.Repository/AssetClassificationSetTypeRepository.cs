using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DataManagement.Entities;
using DataManagement.Repository.Interfaces;

namespace DataManagement.Repository
{
    public class AssetClassificationSetTypeRepository : BaseRepository, IRepository<AssetClassificationSetType>
    {
        public IEnumerable<AssetClassificationSetType> Get()
        {
            IList<AssetClassificationSetType> assetClassificationSetType = SqlMapper.Query<AssetClassificationSetType>(con, "SELECT ACST.IdAssetClassificationSetType\r\n\t\t,ACST.AssetClassificationSetTypeName\r\n\t\t,ACST.AssetClassificationSetTypeShortName\r\n\t\t,ACST.IsDeleted FROM CCO.AssetClassificationSetType AS ACST", commandType: System.Data.CommandType.Text).ToList();
            return assetClassificationSetType;
        }

        AssetClassificationSetType IRepository<AssetClassificationSetType>.Get(int id)
        {
            throw new NotImplementedException();
        }


        public void Add(AssetClassificationSetType entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(AssetClassificationSetType entity)
        {
            throw new NotImplementedException();
        }



        public IEnumerable<AssetClassificationSetType> GetTransactionsForDateRange(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AssetClassificationSetType> Get(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            throw new NotImplementedException();
        }
    }
}