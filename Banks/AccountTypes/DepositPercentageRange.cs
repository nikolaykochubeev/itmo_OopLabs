using Banks.Tools;

namespace Banks.AccountTypes
{
    public abstract class DepositPercentageRange
    {
        protected DepositPercentageRange(decimal from, decimal before, decimal annualPercentage)
        {
            if (Before - From <= 0)
                throw new BanksException("Invalid 'from' and 'before'");
            From = from;
            Before = before;
            AnnualPercentage = annualPercentage;
        }

        public decimal From { get; }
        public decimal Before { get; }
        public decimal AnnualPercentage { get; }
    }
}