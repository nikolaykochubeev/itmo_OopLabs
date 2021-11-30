using System;
using System.Transactions;
using Banks.Interfaces;

namespace Banks.TransactionTypes
{
    public class MoneyTransferTransaction : ITransaction
    {
        public Transaction Create(ITransaction transactionType, decimal amountOfMoney, params IBankAccount[] bankAccounts)
        {
            throw new NotImplementedException();
        }

        public void Cancel()
        {
            throw new NotImplementedException();
        }
    }
}