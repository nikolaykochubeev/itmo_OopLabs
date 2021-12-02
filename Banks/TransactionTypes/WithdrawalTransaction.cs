using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Interfaces;
using Banks.Tools;

namespace Banks.TransactionTypes
{
    public class WithdrawalTransaction : ITransaction
    {
        private decimal _amountOfMoney;
        private List<Guid> _bankAccounts;
        public Guid Id { get; } = Guid.NewGuid();
        public bool IsCanceled { get; private set; }
        public ITransaction Create(decimal amountOfMoney, List<IBankAccount> bankAccounts)
        {
            _bankAccounts = bankAccounts.Select(accounts => accounts.Id()).ToList();
            _amountOfMoney = amountOfMoney;
            bankAccounts.Select(account => account.Withdraw(amountOfMoney));
            return this;
        }

        public ITransaction Cancel(List<IBankAccount> bankAccounts)
        {
            if (IsCanceled)
            {
                throw new BanksException("Transaction already canceled");
            }

            bankAccounts.Select(account => account.TopUp(_amountOfMoney));
            IsCanceled = true;
            return this;
        }

        Guid ITransaction.GetId()
        {
            return Id;
        }

        public IReadOnlyList<Guid> GetBankAccountsId()
        {
            return _bankAccounts;
        }
    }
}