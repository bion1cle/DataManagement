using System;
using System.Collections.Generic;

namespace DataManagement.Business.Interfaces
{
    public interface IManager<T> where T : class
    {
        void LoadData();
        void LoadData(DateTime start, DateTime end, int idPortfolio);
        IEnumerable<T> Data { get; set; }

    }
}