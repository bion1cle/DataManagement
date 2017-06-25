using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataManagement.Business.Interfaces;
using DataManagement.Entities;
using DataManagement.Repository.Interfaces;

namespace DataManagement.Business
{
    public class TransactionFXSManager : BaseManager<TransactionFXS>, IManager<TransactionFXS>
    {
        public TransactionFXSManager(IRepository<TransactionFXS> repository) : base(repository)
        {
        }

        public void LoadData()
        {
            throw new NotImplementedException();
        }

        public void LoadData(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            Data = base.Repository.Get(start, end, idPortfolio, idPortfolioList);
            FXDataGenerate();
        }
        private void FXDataGenerate()
        {
            var fxsQuery = Data.GroupBy(s => new
            {
                s.TradeDate,
                s.IdSecurity,
                s.IdPortfolio,
                s.HoldingKeyLocalGAAP
            }).Select(grp => new FXSValue
            {
                IdSecurity = grp.Key.IdSecurity
                ,
                IdPortfolio = grp.Key.IdPortfolio
                ,
                TradeDate = grp.Key.TradeDate
                ,
                HoldingKeyLocalGAAP = grp.Key.HoldingKeyLocalGAAP
                ,
                Perf = grp.Sum(x => x.Perf)
            });
            //foreach (var fx in fxsQuery.ToList())
            //{
            //    Console.WriteLine(fx.IdSecurity);
            //    Console.WriteLine(fx.Perf);
            //}
            FXData = fxsQuery.ToList();
        }

        public IEnumerable<TransactionFXS> Data { get; set; }

        public IEnumerable<FXSValue> FXData {
            get;set;
        }

    }

    public class FXSValue
    {
        public int IdSecurity { get; set; }
        public int IdPortfolio { get; set; }
        public DateTime TradeDate { get; set; }
        public int HoldingKeyLocalGAAP { get; set; }
        public double? Perf { get; set; }
    }
}
