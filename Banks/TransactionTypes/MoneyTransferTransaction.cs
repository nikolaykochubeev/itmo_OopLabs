using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Interfaces;
using Banks.Tools;

namespace Banks.TransactionTypes
{
    public class MoneyTransferTransaction : ITransaction
    {
        private const int WithdrawalAccount = 0;
        private const int TopUpAccount = 1;
        private decimal _amountOfMoney;
        private List<Guid> _bankAccounts;
        public IReadOnlyList<Guid> BankAccounts => _bankAccounts;
        public Guid Id { get; } = Guid.NewGuid();
        public bool IsCanceled { get; private set; }
        public ITransaction Create(decimal amountOfMoney, List<IBankAccount> bankAccounts)
        {
            if (bankAccounts.Count != 2)
            {
                throw new BanksException("When transferring between two accounts, there must be two accounts");
            }

            _amountOfMoney = amountOfMoney;
            bankAccounts[WithdrawalAccount].Withdraw(amountOfMoney);
            bankAccounts[TopUpAccount].TopUp(amountOfMoney);

            _bankAccounts = bankAccounts.Select(accounts => accounts.Id()).ToList();

            return this;
        }

        public ITransaction Cancel(List<IBankAccount> bankAccounts)
        {
            if (IsCanceled)
            {
                throw new BanksException("Transaction already canceled");
            }

            if (bankAccounts.Count != 2)
            {
                throw new BanksException("When transferring between two accounts, there must be two accounts");
            }

            bankAccounts[WithdrawalAccount].TopUp(_amountOfMoney);
            bankAccounts[TopUpAccount].Withdraw(_amountOfMoney);
            IsCanceled = true;
            return this;
        }

        Guid ITransaction.GetId()
        {
            return Id;
        }

        IReadOnlyList<Guid> ITransaction.GetBankAccountsId()
        {
            return BankAccounts;
        }
    }
}