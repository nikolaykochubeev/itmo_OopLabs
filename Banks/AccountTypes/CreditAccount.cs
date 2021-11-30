using System;
using Banks.Interfaces;

namespace Banks.AccountTypes
{
    public class CreditAccount : IBankAccount
    {
        private const uint NumberOfDaysInYear = 365;
        public CreditAccount(Guid clientId, decimal annualPercentage)
        {
            ClientId = clientId;
            AnnualPercentage = annualPercentage;
            Id = Guid.NewGuid();
            AmountOfMoney = default;
        }

        public Guid Id { get; }
        public decimal AnnualPercentage { get; }
        public Guid ClientId { get; }
        public decimal AmountOfMoney { get; private set; }
        public uint TotalNumberOfDays { get; private set; }

        public IBankAccount TopUp(decimal amountOfMoney)
        {
            AmountOfMoney += amountOfMoney;
            return this;
        }

        public IBankAccount Withdraw(decimal amountOfMoney)
        {
            AmountOfMoney -= amountOfMoney;
            return this;
        }

        public IBankAccount WasteTime(uint days)
        {
            if (AmountOfMoney < 0)
            {
                AmountOfMoney -= days * AnnualPercentage / NumberOfDaysInYear;
            }

            TotalNumberOfDays += days;
            return this;
        }
    }
}