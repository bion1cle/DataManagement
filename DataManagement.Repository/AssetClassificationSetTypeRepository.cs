using System;
using System.Collections.Generic;
using DataManagement.Entities;
using DataManagement.Repository.Interfaces;

namespace DataManagement.Repository
{
    public class AssetClassificationSetTypeRepository : BaseRepository, IRepository<AssetClassificationSetType>
    {
        public IEnumerable<AssetClassificationSetType> Get()
        {
            throw new NotImplementedException();
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