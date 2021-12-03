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
        private List<DepositPercentageRange> _depositRanges;
        private decimal _moneyBuffer;
        public DepositAccount(Guid clientId, uint accountExpirationDate, List<DepositPercentageRange> depositRanges)
        {
            ClientId = clientId;
            TotalNumberOfDays = (int)accountExpirationDate;
            Id = Guid.NewGuid();
            _depositRanges = depositRanges;
        }

        public Guid Id { get; }
        public decimal AnnualPercentage { get; private set; }
        public Guid ClientId { get; }
        public decimal AmountOfMoney { get; private set; }
        public int TotalNumberOfDays { get; private set; }
        public uint AccountTerm { get; private set; }
        public IBankAccount TopUp(decimal amountOfMoney)
        {
            AmountOfMoney += amountOfMoney;
            UpdatePercentage();
            return this;
        }

        public IBankAccount Withdraw(decimal amountOfMoney)
        {
            if (TotalNumberOfDays < AccountTerm)
                throw new BanksException("Sorry, term days of this debit account is not over");
            AmountOfMoney -= amountOfMoney;
            UpdatePercentage();
            return this;
        }

        public IBankAccount WasteTime(uint days)
        {
            while (days > 0)
            {
                if (TotalNumberOfDays < AccountTerm)
                    _moneyBuffer += AmountOfMoney * (AnnualPercentage / NumberOfDaysInYear);
                TotalNumberOfDays++;
                if (TotalNumberOfDays % 30 == 0)
                {
                    AmountOfMoney += _moneyBuffer;
                    _moneyBuffer = 0;
                }

                days--;
            }

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

        decimal IBankAccount.AmountOfMoney()
        {
            return AmountOfMoney;
        }

        private void UpdatePercentage()
        {
            DepositPercentageRange depositPercentageRange = _depositRanges.FirstOrDefault(range => range.From <= AmountOfMoney && range.Before >= AmountOfMoney);
            AnnualPercentage = depositPercentageRange.AnnualPercentage;
        }
    }
}