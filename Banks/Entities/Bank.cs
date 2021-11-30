using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Entities;
using Banks.Interfaces;

namespace Banks.Entities
{
    public class Bank : ICentralBank
    {
        private List<IBankAccount> _bankAccounts = new ();

        public Bank()
        {
            throw new NotImplementedException();
        }

        public Guid CreateBankAccount(Client client)
        {
            throw new NotImplementedException();
        }

        public decimal GetAmountOfMoneyOnBankAccount(Guid bankAccountId)
        {
            throw new NotImplementedException();
        }

        public Guid CreateTransaction(ITransaction transactionType, decimal amountOfMoney, params Guid[] bankAccounts)
        {
            throw new NotImplementedException();
        }

        public void CancelTransaction(Guid transactionId)
        {
            throw new NotImplementedException();
        }

        public void WasteTimeMechanism(uint days)
        {
            foreach (IBankAccount bankAccount in _bankAccounts)
            {
                bankAccount.WasteTime(days);
            }
        }
    }
}