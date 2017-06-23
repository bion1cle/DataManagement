using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
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