using System;
using System.Transactions;
using Banks.Entities;

namespace Banks.Interfaces
{
    public interface ITransaction
    {
        Transaction Create(ITransaction transactionType, decimal amountOfMoney, params IBankAccount[] bankAccounts);
        void Cancel();
    }
}