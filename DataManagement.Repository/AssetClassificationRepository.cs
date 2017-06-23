using System;
using System.Collections.Generic;
using DataManagement.Entities;
using DataManagement.Repository.Interfaces;

namespace DataManagement.Repository
{
    public class AssetClassificationRepository : BaseRepository, IRepository<AssetClassification>
    {
        public IEnumerable<AssetClassification> Get()
        {
            throw new NotImplementedException();
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