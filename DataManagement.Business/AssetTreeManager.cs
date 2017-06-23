using System;
using System.Collections.Generic;
using DataManagement.Business.Interfaces;
using DataManagement.Entities;
using DataManagement.Repository.Interfaces;

namespace DataManagement.Business
{
    public class AssetTreeManager : ManagerBase<AssetTree>, IManager<AssetTree>
    {
        public AssetTreeManager(IRepository<AssetTree> repository) : base(repository)
        {
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