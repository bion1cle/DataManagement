using System;
using System.Collections.Generic;
using System.Linq;
using DataManagement.Business.Interfaces;
using DataManagement.Entities;
using DataManagement.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DataManagement.Business
{
    public class ContributionManager
    {
        private IServiceProvider _serviceProvider = null;
        public PortfolioListManager _portfolioListManager = null;
        public TransactionManager _transactionManager = null;
        public TransactionsBankAccountsManager _transactionsBankAccountsManager = null;
        public DailyReturnsManager _dailyReturnsManager = null;

        public ContributionManager(IManager<PortfolioList> portfolioListManager
            , IManager<Transaction> transactionManager
            , IManager<TransactionsBankAccounts> transactionBankAccountsManager
            , IManager<DailyReturns> dailyReturnsManager)
        {
            _portfolioListManager = portfolioListManager as PortfolioListManager;
            _transactionManager = transactionManager as TransactionManager;
            _transactionsBankAccountsManager = transactionBankAccountsManager as TransactionsBankAccountsManager;
            _dailyReturnsManager = dailyReturnsManager as DailyReturnsManager;
            //_serviceProvider = new ServiceCollection()
            //    //.AddLogging()
            //    //.AddSingleton<IFooService, FooService>()
            //    //.AddSingleton<IBarService, BarService>()
            //    .AddTransient<IRepository<Portfolio>, PortfolioRepository>()
            //    .AddTransient<IRepository<PortfolioList>, PortfolioListRepository>()
            //    .AddTransient<IManager<PortfolioList>, PortfolioListManager>()
            //    .BuildServiceProvider();
        }

        public void Init()
        {
            _portfolioListManager.LoadData();
            foreach (var l in _portfolioListManager.Data)
            {
                Console.WriteLine(l.Portfolio);
            }

            DateTime start = new DateTime(2017, 4, 1);
            DateTime end = new DateTime(2017, 5, 31);
            int idPortfolio = 41;

            _transactionManager.LoadData(start, end, idPortfolio, null);
            foreach (var l in _transactionManager.Data)
            {
                Console.WriteLine(l.HoldingKeyLocalGAAP);
            }
            _transactionsBankAccountsManager.LoadData(start, end, idPortfolio, null);
            foreach (var t in _transactionsBankAccountsManager.Data)
            {
                Console.WriteLine(t.HoldingKeyLocalGAAP + "----" + t.SecurityShortName);
            }

            _dailyReturnsManager.LoadData(start, end, idPortfolio, null);
            foreach (var d in _dailyReturnsManager.Data.Where(s=>s.SecurityShortName == "FXF16600106"))
            {
                Console.WriteLine("{0}---{1}---{2}---{3}", d.PortfolioShortName, d.SecurityShortName, d.Date.ToString("yyyyMMdd"), d.DirtyValuePC);                            
            }

            var query = new List<DailyReturns>();
            foreach (var t1 in _transactionManager.Data.GroupJoin(_dailyReturnsManager.Data, t => new
            {
                HoldingKey = t.HoldingKeyLocalGAAP,
                TradeDateT = t.TradeDate,
                LegNumber = t.Legnumber
            }, dr => new
            {
                HoldingKey = dr.HOLKEYIK,
                TradeDateT = dr.Date,
                LegNumber = dr.LegNumber
            }, (t, drt) => new {t, drt}))
            foreach (var abc in t1.drt)
            {
                if (abc.BalanceNominalNumber == 0)
                    query.Add(new DailyReturns
                    {
                        SecurityShortName = abc.SecurityShortName,
                        WeightYest = abc.WeightYest,
                        Date = abc.Date,
                        DirtyValuePC = abc.DirtyValuePC,
                        CleanValuePC = abc.CleanValuePC,
                        DailyPerfromance = abc.DailyPerfromance,
                        HOLKEYIK = abc.HOLKEYIK,
                        BalanceNominalNumber = abc.BalanceNominalNumber,
                        PortfolioValue = abc.PortfolioValue,
                        PriceCleanQC = abc.PriceCleanQC,
                        IdPortfolio = abc.IdPortfolio,
                        IdSecurity = abc.IdSecurity,
                        AssetClass = abc.AssetClass,
                        InvestmentTyp = abc.InvestmentTyp,
                        PortfolioGroupShortName = abc.PortfolioGroupShortName,
                        PortfolioShortName = abc.PortfolioShortName,
                        InstrumentTypeName = abc.InstrumentTypeName,
                        IdPositionCurrency = abc.IdPositionCurrency,
                        LegNumber = abc.LegNumber,
                        FXRateQCPC = abc.FXRateQCPC
                    });
            }
            Console.WriteLine("---------------------------------------");
            Console.WriteLine(_dailyReturnsManager.Data.Count());
            var newCol = _dailyReturnsManager.Data.ToList();
            foreach (var d in query.ToList())
            {
                Console.WriteLine("{0}---{1}---{2}---{3}", d.PortfolioShortName, d.SecurityShortName, d.Date.ToString("yyyyMMdd"), d.DirtyValuePC);
                //_dailyReturnsManager.Data.Append(d);
                newCol.Add(d);
            }
            Console.WriteLine(_dailyReturnsManager.Data.Count());
            Console.WriteLine(newCol.Count);
            _dailyReturnsManager.Data = newCol;
            Console.WriteLine(_dailyReturnsManager.Data.Count());


        }

        public void CalcRun1()
        {

        }
    }
}