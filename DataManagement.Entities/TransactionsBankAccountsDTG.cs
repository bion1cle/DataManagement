using System;

namespace DataManagement.Entities
{
    public class TransactionsBankAccountsDTG
    {
        public string ElementaryTransCodeShortName
        {
            get; set;
        }

        public string SecurityShortName
        {
            get; set;
        }

        public double BalanceNominalQC
        {
            get; set;
        }

        public double CurrentValuePC
        {
            get; set;
        }

        public double PaymentAmountPC
        {
            get; set;
        }

        public double TradePriceQC
        {
            get; set;
        }

        public string BusinessTransCodeName
        {
            get; set;
        }

        public DateTime TradeDate
        {
            get; set;
        }

        public int HoldingKeyLocalGAAP
        {
            get; set;
        }

        public string FCName
        {
            get; set;
        }

        public int IdBankAccount
        {
            get; set;
        }

        public DateTime PaymentDate
        {
            get; set;
        }
    }
}