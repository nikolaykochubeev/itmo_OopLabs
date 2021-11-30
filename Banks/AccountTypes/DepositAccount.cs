using System;
using Banks.Interfaces;

namespace Banks.AccountTypes
{
    public class DepositAccount : IBankAccount
    {
        private const uint NumberOfDaysInYear = 365;
        public DepositAccount(Guid clientId, uint term)
        {
            ClientId = clientId;
            Term = term;
            Id = Guid.NewGuid();
            AmountOfMoney = default;
        }

        public Guid Id { get; }
        public decimal AnnualPercentage { get; }
        public Guid ClientId { get; }
        public decimal AmountOfMoney { get; private set; }
        public uint Term { get; }
        public uint TotalNumberOfDays { get; private set; }

        public IBankAccount TopUp(decimal amountOfMoney)
        {
            throw new System.NotImplementedException();
        }

        public IBankAccount Withdraw(decimal amountOfMoney)
        {
            throw new System.NotImplementedException();
        }

        public IBankAccount WasteTime(uint days)
        {
            throw new System.NotImplementedException();
        }
    }
}