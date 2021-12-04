using System;
using Banks.Interfaces;
using Banks.Tools;

namespace Banks.AccountTypes
{
    public class DebitAccount : IBankAccount
    {
        private const uint NumberOfDaysInYear = 365;

        public DebitAccount(Guid clientId, decimal annualPercentage)
        {
            ClientId = clientId;
            AnnualPercentage = annualPercentage;
            Id = Guid.NewGuid();
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
            if ((AmountOfMoney - amountOfMoney) < 0)
            {
                throw new BanksException("On a debit account you cannot go to a negative value");
            }

            AmountOfMoney -= amountOfMoney;

            return this;
        }

        public IBankAccount WasteTime(uint days)
        {
            AmountOfMoney += days * AnnualPercentage / NumberOfDaysInYear;
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