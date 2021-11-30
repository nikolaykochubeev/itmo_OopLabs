using System;
using Banks.Entities;

namespace Banks.Interfaces
{
    public interface ICentralBank
    {
        Guid CreateBankAccount(Client client);
        decimal GetAmountOfMoneyOnBankAccount(Guid bankAccountId);
        Guid CreateTransaction(ITransaction transactionType, decimal amountOfMoney, params Guid[] bankAccounts);
        void CancelTransaction(Guid transactionId);
    }
}