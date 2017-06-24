using System;
using System.Collections.Generic;
using DataManagement.Business.Interfaces;
using DataManagement.Entities;
using DataManagement.Repository.Interfaces;

namespace DataManagement.Business
{
    public class DailyReturnsManager : BaseManager<DailyReturns>, IManager<DailyReturns>
    {
        public DailyReturnsManager(IRepository<DailyReturns> repository) : base(repository)
        {
        }

        public void LoadData()
        {
            throw new NotImplementedException();
        }

        public void LoadData(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            this.Data = base.Repository.Get(start, end, idPortfolio, idPortfolioList);
        }
        

        public IEnumerable<DailyReturns> Data { get; set; }
    }
}