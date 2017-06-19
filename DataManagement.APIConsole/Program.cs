using System;
using System.Collections.Generic;
using DataManagement.Business;
using DataManagement.Business.Interfaces;
using DataManagement.Entities;

using DataManagement.Repository;
using DataManagement.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DataManagement.APIConsole
{
    class Program
    {
        IRepository<Portfolio> _portfolioRepository = null;
        private IServiceProvider serviceProvider = null;
        static void Main(string[] args)
        {
            
            Console.WriteLine("Hello World!");
            var serviceProvider = new ServiceCollection()
                //.AddLogging()
                //.AddSingleton<IFooService, FooService>()
                //.AddSingleton<IBarService, BarService>()
                .AddTransient<IRepository<Portfolio>, PortfolioRepository>()
                .AddTransient<IRepository<PortfolioList>, PortfolioListRepository>()
                .AddTransient<IManager<Transaction>, TransactionManager>()
                .AddTransient<IManager<PortfolioList>, PortfolioListManager>()
                .AddTransient<IRepository<Transaction>, TransactionRepository>()
                .AddTransient<IRepository<TransactionsBankAccounts>, TransactionsBankAccountsRepository>()
                .AddTransient<IManager<TransactionsBankAccounts>, TransactionsBankAccountsManager>()
                .AddTransient<IRepository<DailyReturns>, DailyReturnsRepository>()
                .AddTransient<IManager<DailyReturns>, DailyReturnsManager>()

                .BuildServiceProvider();

            //var prog = serviceProvider.GetService<IRepository<Portfolio>>();
            //foreach (var ppp in prog.Get())
            //{
            //    Console.WriteLine(ppp.PortfolioShortName);
            //}

            //var progList = serviceProvider.GetService<IRepository<PortfolioList>>();
            //foreach (var ppp in progList.Get())
            //{
            //    Console.WriteLine(ppp.Portfolio);
            //}

            //Console.WriteLine("------------------------------------");     

            ContributionManager contri = new ContributionManager(serviceProvider.GetService<IManager<PortfolioList>>()
                                                                , serviceProvider.GetService<IManager<Transaction>>()
                                                                , serviceProvider.GetService<IManager<TransactionsBankAccounts>>()
                                                                , serviceProvider.GetService<IManager<DailyReturns>>()
                                                                );
            //ContributionManager contri = new ContributionManager();
            //contri._portfolioListManager = serviceProvider.GetService<IManager<PortfolioList>>() as PortfolioListManager;
            //var t = serviceProvider.GetService<IManager<Transaction>>();
            //contri._transactionManager = serviceProvider.GetService<IManager<Transaction>>() as TransactionManager;

            contri.Init();

        }
        

        public Program(IRepository<Portfolio> portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        protected void ShowAll()
        {
            foreach (var p in _portfolioRepository.Get())
            {
                Console.WriteLine(p.PortfolioShortName);
            }
        }
    }
}