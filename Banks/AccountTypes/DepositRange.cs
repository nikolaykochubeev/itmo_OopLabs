namespace Banks.AccountTypes
{
    public class DepositRange
    {
        public DepositRange(decimal from, decimal before, decimal annualPercentage)
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