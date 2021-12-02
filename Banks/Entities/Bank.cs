using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Banks.Entities;
using Banks.Interfaces;
using Banks.Tools;

namespace Banks.Entities
{
    public class Bank : ICentralBank
    {
        private readonly List<Client> _clients = new ();
        private readonly List<IBankAccount> _bankAccounts = new ();
        private readonly List<ITransaction> _transactions = new ();
        public Bank(BankSettings bankSettings)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<Client> Clients => _clients;
        public IReadOnlyList<IBankAccount> BankAccounts => _bankAccounts;

        public Guid CreateBankAccount(Client client)
        {
            throw new NotImplementedException();
        }

        public Guid CreateTransaction(ITransaction transactionType, decimal amountOfMoney, params Guid[] bankAccountsId)
        {
            List<IBankAccount> bankAccounts = GetBankAccounts(bankAccountsId.ToList());

            ITransaction transaction = transactionType.Create(amountOfMoney, bankAccounts);
            _transactions.Add(transaction);
            return transaction.GetId();
        }

        public Guid CancelTransaction(Guid transactionId)
        {
            ITransaction transaction = _transactions.FirstOrDefault(transaction => transaction.GetId() == transactionId);

            if (transaction is null)
            {
                throw new BanksException("Transaction doesnt exists");
            }

            _transactions.Remove(transaction);
            transaction = transaction.Cancel(CentralBank.GetBankAccounts(transaction.GetBankAccountsId()).ToList());
            _transactions.Add(transaction);
            return transaction.GetId();
        }

        public void WasteTimeMechanism(uint days)
        {
            foreach (IBankAccount bankAccount in _bankAccounts)
            {
                bankAccount.WasteTime(days);
            }
        }

        private List<IBankAccount> GetBankAccounts(List<Guid> bankAccountsId)
        {
            List<IBankAccount> bankAccounts = new ();
            foreach (Guid bankAccountId in bankAccountsId)
            {
                IBankAccount bankAccount = _bankAccounts.FirstOrDefault(account => bankAccountId == account.ClientId());
                bankAccount ??= CentralBank.GetBankAccount(bankAccountId) ?? throw new BanksException("BankAccount doesn't exists");
                bankAccounts.Add(bankAccount);
            }

            return bankAccounts;
        }
    }
}