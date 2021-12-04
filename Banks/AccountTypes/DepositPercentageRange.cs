using Banks.Tools;

namespace Banks.AccountTypes
{
    public class DepositPercentageRange
    {
        public DepositPercentageRange(decimal from, decimal before, decimal annualPercentage)
        {
            From = from;
            Before = before;
            AnnualPercentage = annualPercentage;
        }

        public decimal From { get; }
        public decimal Before { get; }
        public decimal AnnualPercentage { get; }
    }
}