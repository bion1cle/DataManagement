using System;
using System.Collections.Generic;
using DataManagement.Business.Interfaces;
using DataManagement.Entities;
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

        public ContributionManager(IManager<PortfolioList> portfolioListManager, IManager<Transaction> transactionManager)
        {
            _portfolioListManager = portfolioListManager as PortfolioListManager;
            _transactionManager = transactionManager as TransactionManager;
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
            int idPortfolio = 35;

            _transactionManager.LoadData(start, end, idPortfolio);
            foreach (var l in _transactionManager.Data)
            {
                Console.WriteLine(l.HoldingKeyLocalGAAP);
            }
        }
    }
}