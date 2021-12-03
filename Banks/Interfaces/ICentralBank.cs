using System;
using System.Collections.Generic;
using Banks.Entities;

namespace Banks.Interfaces
{
    public interface ICentralBank
    {
        public Client AddClient(string firstName, string lastName);
        public Guid CreateDebitAccount(Client client);

        public Guid CreateDepositAccount(Client client);

        public Guid CreateCreditAccount(Client client);

        public Guid CreateTransaction(ITransaction transactionType, decimal amountOfMoney, params Guid[] bankAccountsId);
        public Guid CancelTransaction(Guid transactionId);
        public void WasteTimeMechanism(uint days);
        public List<IBankAccount> GetBankAccounts(List<Guid> bankAccountsId);
    }
}