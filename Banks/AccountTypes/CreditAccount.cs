using System;
using Banks.Interfaces;
using Banks.Tools;

namespace Banks.AccountTypes
{
    public class CreditAccount : IBankAccount
    {
        private const uint NumberOfDaysInYear = 365;

        public CreditAccount(Guid clientId, decimal annualPercentage, decimal creditLimit)
        {
            ClientId = clientId;
            AnnualPercentage = annualPercentage;
            CreditLimit = creditLimit;
        }

        public Guid Id { get; } = Guid.NewGuid();
        public decimal AnnualPercentage { get; }
        public Guid ClientId { get; }
        public decimal AmountOfMoney { get; private set; }
        public uint TotalNumberOfDays { get; private set; }
        public decimal CreditLimit { get; private set; }
        public IBankAccount TopUp(decimal amountOfMoney)
        {
            AmountOfMoney += amountOfMoney;
            return this;
        }

        public IBankAccount Withdraw(decimal amountOfMoney)
        {
            if (AmountOfMoney < CreditLimit)
                throw new BanksException("Credit limit exceeded");
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

        Guid IBankAccount.Id()
        {
            return Id;
        }

        Guid IBankAccount.ClientId()
        {
            return ClientId;
        }

        decimal IBankAccount.AmountOfMoney()
        {
            return AmountOfMoney;
        }
    }
}