using System;
using System.Collections.Generic;

namespace Banks.Interfaces
{
    public interface ITransaction
    {
        ITransaction Create(decimal amountOfMoney, List<IBankAccount> bankAccounts);
        ITransaction Cancel(List<IBankAccount> bankAccounts);
        Guid GetId();
        IReadOnlyList<Guid> GetBankAccountsId();
    }
}