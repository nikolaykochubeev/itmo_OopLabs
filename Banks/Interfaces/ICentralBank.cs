using System;
using Banks.Entities;

namespace Banks.Interfaces
{
    public interface ICentralBank
    {
        Guid CreateBankAccount(Client client);
        Guid CreateTransaction(ITransaction transactionType, decimal amountOfMoney, params Guid[] bankAccountsId);
        Guid CancelTransaction(Guid transactionId);
    }
}