using System;

namespace DataManagement.Entities
{
    public class TransactionFXS
    {
        public int RowNum { get; set; }

        public string PurposeOfUse { get; set; }
        public double CurrentValuePC { get; set; }
        public double PaymentAmountPC { get; set; }
        public string SecurityShortName { get; set; }
        public string ElementaryTransCodeShortName { get; set; }
        public int IdSecurity { get; set; }
        public int IdPortfolio { get; set; }
        public int HoldingKeyLocalGAAP { get; set; }
        public DateTime TradeDate { get; set; }
        public ulong LocalTransID { get; set; }
        public bool ValidYN { get; set; }
        public double Perf { get; set; }
    }
}