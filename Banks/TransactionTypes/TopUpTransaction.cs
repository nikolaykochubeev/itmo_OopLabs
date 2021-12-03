using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Entities;
using Banks.Interfaces;
using Banks.Tools;

namespace Banks.TransactionTypes
{
    public class TopUpTransaction : ITransaction
    {
        private decimal _amountOfMoney;
        private List<Guid> _bankAccounts;
        public IReadOnlyList<Guid> BankAccounts => _bankAccounts;
        public Guid Id { get; } = Guid.NewGuid();
        public bool IsCanceled { get; private set; }
        public ITransaction Create(decimal amountOfMoney, List<IBankAccount> bankAccounts)
        {
            _bankAccounts = bankAccounts.Select(accounts => accounts.Id()).ToList();
            _amountOfMoney = amountOfMoney;
            foreach (IBankAccount account in bankAccounts)
            {
                account.TopUp(_amountOfMoney);
            }

            return this;
        }

        public ITransaction Cancel(List<IBankAccount> bankAccounts)
        {
            if (IsCanceled)
            {
                throw new BanksException("Transaction already canceled");
            }

            foreach (IBankAccount account in bankAccounts)
            {
                account.Withdraw(_amountOfMoney);
            }

            IsCanceled = true;
            return this;
        }

        Guid ITransaction.GetId()
        {
            return Id;
        }

        public IReadOnlyList<Guid> GetBankAccountsId()
        {
            return BankAccounts;
        }
    }
}