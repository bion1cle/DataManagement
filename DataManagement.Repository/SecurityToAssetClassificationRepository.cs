using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DataManagement.Entities;
using DataManagement.Repository.Interfaces;

namespace DataManagement.Repository
{
    public class SecurityToAssetClassificationRepository : BaseRepository, IRepository<SecurityToAssetClassification>
    {
        public IEnumerable<SecurityToAssetClassification> Get()
        {
            IList<SecurityToAssetClassification> securityAssetList = SqlMapper.Query<SecurityToAssetClassification>(con, "RIED.uspPerformanceContributionSecurityToAssetClass", commandType: System.Data.CommandType.StoredProcedure).ToList();
            return securityAssetList;
        }

        public SecurityToAssetClassification Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(SecurityToAssetClassification entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(SecurityToAssetClassification entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SecurityToAssetClassification> GetTransactionsForDateRange(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SecurityToAssetClassification> Get(DateTime start, DateTime end, int? idPortfolio, int? idPortfolioList)
        {
            throw new NotImplementedException();
        }
    }
}