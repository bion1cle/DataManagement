using System;
using System.Collections.Generic;
using DataManagement.Business.Interfaces;
using DataManagement.Entities;
using DataManagement.Repository.Interfaces;

namespace DataManagement.Business
{
    public class PortfolioListManager : IManager<PortfolioList>
    {
        private readonly IRepository<PortfolioList> _portfolioListRepository;
        public PortfolioListManager(IRepository<PortfolioList> portfolioListRepository)
        {
            _portfolioListRepository = portfolioListRepository ?? throw new ArgumentNullException(nameof(portfolioListRepository));            
        }
        public void LoadData()
        {
            this.Data = _portfolioListRepository.Get();
        }

        public void LoadData(DateTime start, DateTime end, int idPortfolio)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PortfolioList> Data { get; set; }
    }
}