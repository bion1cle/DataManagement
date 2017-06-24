using System;
using System.Collections.Generic;
using System.Text;

namespace DataManagement.Entities
{
    public class PositionFactData
    {
        public int IdPosition { get; set; }
        public int IdPortfolio { get; set; }
        public int IdSecurity { get; set; }
        public double DirtyValuePC { get; set; }
        public DateTime Date { get; set; }
        public double BalanceNominalNumber { get; set; }
    }
}
