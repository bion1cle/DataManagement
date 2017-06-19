using System;

namespace DataManagement.Entities
{
    public class DailyReturns
    {
        public string SecurityShortName
        {
            get; set;
        }

        public double WeightYest
        {
            get; set;
        }

        public DateTime Date
        {
            get; set;
        }

        public double DirtyValuePC
        {
            get; set;
        }

        public double CleanValuePC
        {
            get; set;
        }

        public double DailyPerfromance
        {
            get; set;
        }
        public int HOLKEYIK
        {
            get; set;
        }

        public double BalanceNominalNumber
        {
            get; set;
        }

        public double PortfolioValue
        {
            get; set;
        }

        public double PriceCleanQC
        {
            get; set;
        }
        public int IdPortfolio
        {
            get; set;
        }
        public int IdSecurity
        {
            get; set;
        }

        public string AssetClass
        {
            get; set;
        }
        public string InvestmentTyp
        {
            get; set;
        }
        public string PortfolioGroupShortName
        {
            get; set;
        }
        public string PortfolioShortName
        {
            get; set;
        }
        public string InstrumentTypeName
        {
            get; set;
        }
        public int IdPositionCurrency
        {
            get; set;
        }
        public int LegNumber
        {
            get; set;
        }

        public double FXRateQCPC
        {
            get; set;
        }

    }
}