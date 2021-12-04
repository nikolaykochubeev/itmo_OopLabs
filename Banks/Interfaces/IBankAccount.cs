using System;
using Banks.Entities;

namespace Banks.Interfaces
{
    public interface IBankAccount
    {
        IBankAccount TopUp(decimal amountOfMoney);
        IBankAccount Withdraw(decimal amountOfMoney);
        IBankAccount WasteTime(uint days);
        Guid Id();
        Guid ClientId();
        decimal AmountOfMoney();
    }
}