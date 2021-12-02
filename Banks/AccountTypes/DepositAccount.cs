using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Interfaces;
using Banks.Tools;

namespace Banks.AccountTypes
{
    public class DepositAccount : IBankAccount
    {
        private const uint NumberOfDaysInYear = 365;
        private const int ConstWhenTermEnds = 0;
        private List<DepositRange> _depositRanges;
        public DepositAccount(Guid clientId, uint term, List<DepositRange> depositRanges)
        {
            ClientId = clientId;
            Term = (int)term;
            Id = Guid.NewGuid();
            _depositRanges = depositRanges;
        }

        public Guid Id { get; }
        public decimal AnnualPercentage { get; private set; }
        public Guid ClientId { get; }
        public decimal AmountOfMoney { get; private set; }
        public int Term { get; private set; }
        public uint TotalNumberOfDays { get; private set; }

        public IBankAccount TopUp(decimal amountOfMoney)
        {
            AmountOfMoney += amountOfMoney;
            UpdatePercentage();
            return this;
        }

        public IBankAccount Withdraw(decimal amountOfMoney)
        {
            if (Term != 0)
                throw new BanksException("Sorry, term days of this debit account is not over");
            AmountOfMoney -= amountOfMoney;
            UpdatePercentage();
            return this;
        }

        public IBankAccount WasteTime(uint days)
        {
            Term = (int)(Term - days < ConstWhenTermEnds ? ConstWhenTermEnds : Term - days);
            AmountOfMoney += days * AnnualPercentage / NumberOfDaysInYear;
            TotalNumberOfDays += days;
            UpdatePercentage();
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

        private void UpdatePercentage()
        {
            DepositRange depositRange = _depositRanges.FirstOrDefault(range => range.From <= AmountOfMoney && range.Before >= AmountOfMoney);

            AnnualPercentage = depositRange.AnnualPercentage;
        }
    }
}