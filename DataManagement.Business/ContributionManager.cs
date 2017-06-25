using System;
using System.Collections.Generic;
using System.Linq;
using DataManagement.Business.Interfaces;
using DataManagement.Entities;
using DataManagement.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using DataManagement.Common;

namespace DataManagement.Business
{
    public class ContributionManager
    {
        private IServiceProvider _serviceProvider = null;
        private PortfolioListManager _portfolioListManager = null;
        private TransactionManager _transactionManager = null;
        private TransactionsBankAccountsManager _transactionsBankAccountsManager = null;
        private DailyReturnsManager _dailyReturnsManager = null;
        private AssetTreeManager _assetTreeManager = null;
        private PositionFactManager _positionFactManager = null;
        private TransactionFXSManager _transactionFXSManager = null;
        private TransactionsBankAccountsDTGManager _transactionsBankAccountsDTGManager = null;

        DateTime start = new DateTime(2017, 1, 1);
        DateTime end = new DateTime(2017, 5, 31);
        int idPortfolio = 41;

        public ContributionManager(IManager<PortfolioList> portfolioListManager
            , IManager<Transaction> transactionManager
            , IManager<TransactionsBankAccounts> transactionBankAccountsManager
            , IManager<DailyReturns> dailyReturnsManager
            , IManager<AssetTree> assetTreeManager
            , IManager<PositionFactData> positionFactDataManager
            , IManager<TransactionFXS> transactionFXSManager
            , IManager<TransactionsBankAccountsDTG> transactionsBankAccountsDTGManager
            )
        {
            _portfolioListManager = portfolioListManager as PortfolioListManager;
            _transactionManager = transactionManager as TransactionManager;
            _transactionsBankAccountsManager = transactionBankAccountsManager as TransactionsBankAccountsManager;
            _dailyReturnsManager = dailyReturnsManager as DailyReturnsManager;
            _assetTreeManager = assetTreeManager as AssetTreeManager;
            _positionFactManager = positionFactDataManager as PositionFactManager;
            _transactionFXSManager = transactionFXSManager as TransactionFXSManager;
            _transactionsBankAccountsDTGManager =
                transactionsBankAccountsDTGManager as TransactionsBankAccountsDTGManager;
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
                //Console.WriteLine("{0}---{1}---{2}---{3}", d.PortfolioShortName, d.SecurityShortName, d.Date.ToString("yyyyMMdd"), d.DirtyValuePC);
                //_dailyReturnsManager.Data.Append(d);
                newCol.Add(d);
            }
            //Console.WriteLine(_dailyReturnsManager.Data.Count());
            //Console.WriteLine(newCol.Count);
            _dailyReturnsManager.Data = newCol;
            //Console.WriteLine(_dailyReturnsManager.Data.Count());

            _assetTreeManager.LoadData();
            _positionFactManager.LoadData(start, end, null, null);
            _positionFactManager.FillPortfolioValueDictionary(start, idPortfolio);
            _transactionFXSManager.LoadData(start, end, idPortfolio, null);
            
            //_transactionsBankAccountsDTGManager.LoadData(start, end, idPortfolio, null);

        }

        public void CalcRun1()
        {
            //var query = _dailyReturnsManager.Data.OrderBy(p=>p.Date).SelectWithPrevious((prev, cur) =>
            //    new
            //    {
            //        Date = cur.Date
            //        , PortName = cur.PortfolioShortName
            //        , SecName = cur.SecurityShortName
            //        , dirty = cur.DirtyValuePC
            //        , dirtyYest = prev.DirtyValuePC
                    
            //    });

            //foreach (var d in query)
            //{
            //    Console.WriteLine("{0}---{1}---{2}---{3}---{4}", d.Date, d.PortName, d.SecName, d.dirty, d.dirtyYest);
            //}

            //Gib mir mal ne Liste der Papiere
            var query = _dailyReturnsManager.Data//.Where(s=>s.SecurityShortName == "DE0007100000")
                //.Where(w=>w.Date >= start)
                .GroupBy(s => new{
                    s.SecurityShortName
                    , s.IdPortfolio
                    , s.IdPositionCurrency
                    , s.LegNumber
                } )                
                .Select(grp => new {Sec = grp.Key, Items = grp.OrderBy(o=>o.Date).ToList()});
                
            foreach (var d in query)
            {
                Console.WriteLine(d.Sec);
                Console.WriteLine(d.Items.Count);
                //Console.WriteLine("{0}---{1}---{2}---{3}---{4}", d.Items., d.SecurityShortName, "", "", "");
                DailyReturns previous = null;
                foreach (var i in d.Items)
                {
                    if (previous != null)
                    {
                        double clean = CalcCleanPL(i, previous);
                        double portValueToday = 0;
                        double portValueYest = 0;
                        double factor = 0;
                        if (_positionFactManager.PortfolioValueDictionary.ContainsKey(i.Date))
                            portValueToday = _positionFactManager.PortfolioValueDictionary.FirstOrDefault(dd => dd.Key == i.Date).Value.PortfolioValuePerDay;
                        if (_positionFactManager.PortfolioValueDictionary.ContainsKey(previous.Date))
                            portValueYest = _positionFactManager.PortfolioValueDictionary.FirstOrDefault(dd => dd.Key == previous.Date).Value.PortfolioValuePerDay;
                        //_positionFactManager.CalculatePortfolioValue(i.Date, idPortfolio, null);

                        //Faktor ist nichts anderes als die CleanPL / PortfolioValue von gestern +1
                        factor = clean / portValueYest + 1;

                        Console.WriteLine("{0}---{1}---{2}---{3}---{4}---{5}----IdSec:{6}----{7}----{8}----{9}", i.Date, i.PortfolioShortName ,i.SecurityShortName, i.DirtyValuePC, 
                            previous.DirtyValuePC, clean, i.IdSecurity, portValueToday, portValueYest, factor );
                    }
                    previous = i;
                }
            }



            





        }

        private double CalcCleanPL(DailyReturns current, DailyReturns previous )
        {
            double cleanPL = 0;
            Transaction correspondingTransaction = GetCorrespondingTransaction(current);//null;
            FXSValue correspondingTransactionFXS = GetCorrespondingTransactionFXS(current);

            if (previous.DirtyValuePC.HasValue)
            {
                cleanPL = current.DirtyValuePC.Value - previous.DirtyValuePC.Value;
            }
            else
            {
                //correspondingTransaction = GetCorrespondingTransaction(current);
                cleanPL = current.DirtyValuePC.Value -
                          (correspondingTransaction != null ? 0: current.DirtyValuePC.Value);
            }
            if (correspondingTransaction != null) cleanPL -= correspondingTransaction.PaymentAmountPC ?? 0;

            if (current.InvestmentTyp != "Kontokorrent")
            {
                if (correspondingTransaction != null)
                    cleanPL += correspondingTransaction.PaymentAmountPC ?? 0;
                else
                    cleanPL += 0;

            }
            else
            {
                if (previous.DirtyValuePC.HasValue)
                {
                    if (GetCorrespondingTransactionBankAccount(current) != null)
                        cleanPL += GetCorrespondingTransactionBankAccount(current).PaymentAmountPC ?? 0;
                    else
                        cleanPL += 0;
                }
                else
                {
                    cleanPL += 0;
                }
            }
            if (correspondingTransactionFXS != null)
                cleanPL += correspondingTransactionFXS.Perf ?? 0;

            return cleanPL;
        }

        private Transaction GetCorrespondingTransaction(DailyReturns current)
        {
            var query = _transactionManager.Data.Where(
                w => (w.TradeDate == current.Date) && (w.HoldingKeyLocalGAAP == current.HOLKEYIK) &&
                     (w.Legnumber == current.LegNumber));
            return query.FirstOrDefault();
        }
        private TransactionsBankAccounts GetCorrespondingTransactionBankAccount(DailyReturns current)
        {
            var query = _transactionsBankAccountsManager.Data.Where(
                w => (w.TradeDate == current.Date) && (w.IdBankAccount == current.IdSecurity) );
            return query.FirstOrDefault();
        }
        private TransactionsBankAccounts GetCorrespondingTransactionBankAccountDTG(DailyReturns current)
        {
            var query = _transactionsBankAccountsManager.Data.Where(
                w => (w.TradeDate == current.Date) && (w.IdBankAccount == current.IdSecurity));
            return query.FirstOrDefault();
        }
        private FXSValue GetCorrespondingTransactionFXS(DailyReturns current)
        {
            var query = _transactionFXSManager.FXData.Where(
                w => (w.TradeDate == current.Date) && (w.IdSecurity == current.IdSecurity)
                && (w.HoldingKeyLocalGAAP == current.HOLKEYIK)
                );
            return query.FirstOrDefault();
        }
    }
}