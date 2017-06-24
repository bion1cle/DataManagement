using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataManagement.Business.Interfaces;
using DataManagement.Entities;
using DataManagement.Repository.Interfaces;

namespace DataManagement.Business
{
    public class PositionFactManager : BaseManager<PositionFactData>, IManager<PositionFactData>
    {
        public PositionFactManager(IRepository<PositionFactData> repository) : base(repository)
        {
        }

        public void LoadData()
        {
            throw new NotImplementedException();
        }

        public void LoadData(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            Data = base.Repository.Get(start, end, null, null);
        }

        public IEnumerable<PositionFactData> Data { get; set; }

        public double CalculatePortfolioValue(DateTime date, int? idPortfolio, int? idPortfolioList)
        {
            double portfolioValue = 0;
            if (idPortfolio.HasValue)
            {
                var query = Data.ToList().Where(w => (w.Date == date) && (w.IdPortfolio == idPortfolio.Value));
                portfolioValue = query.ToList().Sum(s=>s.DirtyValuePC);
                return portfolioValue;
            }
            else if (idPortfolioList.HasValue)
            {
                return portfolioValue;
            }
            else
                throw new ArgumentOutOfRangeException("idPortfolio || idPortfolioList",
                    "One parameter must have a value");
        }

    }
}
