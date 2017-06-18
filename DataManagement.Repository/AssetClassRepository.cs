using System;
using System.Collections.Generic;
using DataManagement.Entities;
using DataManagement.Repository.Interfaces;

namespace DataManagement.Repository
{
    public class AssetClassRepository: BaseRepository, IRepository<AssetClass>
    {
        public IEnumerable<AssetClass> Get()
        {
            IList<AssetClass> list = new List<AssetClass>()
            {
                new AssetClass(){AssetClassName = "Aktien"},
                new AssetClass(){AssetClassName = "Renten"},
                new AssetClass(){AssetClassName = "Liquidität"},
                new AssetClass(){AssetClassName = "Immobilien"},
                new AssetClass(){AssetClassName = "Rohstoffe"},
                new AssetClass(){AssetClassName = "Währungsprodukte"},
                new AssetClass(){AssetClassName = "Absolute Return"},
                new AssetClass(){AssetClassName = "Private Equity"}
            };
            return list;
        }

        public AssetClass Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Add(AssetClass entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(AssetClass entity)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<AssetClass> GetTransactionsForDateRange(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AssetClass> Get(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            throw new NotImplementedException();
        }
    }
}