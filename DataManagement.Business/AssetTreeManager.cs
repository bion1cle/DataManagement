using System;
using System.Collections.Generic;
using DataManagement.Business.Interfaces;
using DataManagement.Entities;
using DataManagement.Repository.Interfaces;

namespace DataManagement.Business
{
    public class AssetTreeManager : BaseManager<AssetTree>, IManager<AssetTree>
    {
        private IRepository<SecurityToAssetClassification> _secRepository = null;
        public AssetTreeManager(IRepository<AssetTree> repository, IRepository<SecurityToAssetClassification> secRepository) : base(repository)
        {
            _secRepository = secRepository;
        }

        public void LoadData()
        {
            this.Data = base.Repository.Get();

            

        }

        public void LoadData(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AssetTree> Data { get; set; }
    }
}