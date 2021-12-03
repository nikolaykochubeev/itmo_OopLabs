using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Banks.AccountTypes;
using Banks.Entities;
using Banks.Interfaces;
using Banks.Tools;
using Banks.TransactionTypes;

namespace Banks.Entities
{
    public class Bank
    {
        private readonly List<Client> _clients = new ();
        private readonly List<IBankAccount> _bankAccounts = new ();
        private readonly List<ITransaction> _transactions = new ();

        public Bank(string bankName, BankSettings bankSettings)
        {
            if (bankSettings is null)
                throw new BanksException("bankSetting can not be null");
            BankId = bankSettings.BankId;
            BankName = bankName;
            BankSettings = bankSettings;
        }

        public string BankName { get; }
        public Guid BankId { get; }
        public IReadOnlyList<Client> Clients => _clients;
        public IReadOnlyList<IBankAccount> BankAccounts => _bankAccounts;
        public BankSettings BankSettings { get; }
        public Client AddClient(string firstName, string lastName)
        {
            var client = new Client(firstName, lastName);
            _clients.Add(client);
            return client;
        }

        public Client UpdateClientPassport(Guid clientId, ulong passport)
        {
            Client client = _clients.FirstOrDefault(client => client.Id == clientId);
            if (client is null)
                throw new BanksException("Client not found");
            client.UpdateClientPassport(passport);
            UpdateSuspend(client.Id);
            return client;
        }

        public Client UpdateClientAddress(Guid clientId, string address)
        {
            Client client = _clients.FirstOrDefault(client => client.Id == clientId);
            if (client is null)
                throw new BanksException("Client not found");
            client.UpdateClientAddress(address);
            UpdateSuspend(client.Id);
            return client;
        }

        public Client GetClient(Guid clientId)
        {
            return _clients.FirstOrDefault(client => client.Id == clientId);
        }

        public Guid CreateDebitAccount(Guid clientId)
        {
            Client client = _clients.FirstOrDefault(client1 => client1.Id == clientId);
            if (client is null)
                throw new BanksException("client not found");
            var debitAccount = new DebitAccount(client.Id, BankSettings.DebitAnnualPercentage);
            _bankAccounts.Add(debitAccount);
            return debitAccount.Id;
        }

        public Guid CreateDepositAccount(Guid clientId)
        {
            Client client = _clients.FirstOrDefault(client1 => client1.Id == clientId);
            if (client is null)
                throw new BanksException("client not found");
            var debitAccount = new DepositAccount(client.Id, BankSettings.DepositAccountExpirationDate, BankSettings.DepositAnnualPercentages.ToList());
            _bankAccounts.Add(debitAccount);
            return debitAccount.Id;
        }

        public Guid CreateCreditAccount(Guid clientId)
        {
            Client client = _clients.FirstOrDefault(client1 => client1.Id == clientId);
            if (client is null)
                throw new BanksException("client not found");
            var debitAccount = new CreditAccount(client.Id, BankSettings.CreditAnnualPercentage, BankSettings.CreditWithdrawalLimit);
            _bankAccounts.Add(debitAccount);
            return debitAccount.Id;
        }

        public Guid TopUpTransaction(Guid bankAccountId, decimal amountOfMoney)
        {
            IBankAccount bankAccount = _bankAccounts.FirstOrDefault(account => account.Id() == bankAccountId);
            if (bankAccount is null)
                throw new BanksException("bankAccount not found");
            Client client = _clients.FirstOrDefault(client1 => client1.Id == bankAccount.ClientId());
            if (client is null)
                throw new BanksException("client not found");
            UpdateSuspend(client.Id);
            if (client.IsSuspend && amountOfMoney > BankSettings.SuspendTransactionLimit)
                throw new BanksException("suspend transaction limit less than top up money");
            ITransaction transaction = new TopUpTransaction().Create(amountOfMoney, new List<IBankAccount>() { bankAccount });
            _transactions.Add(transaction);
            return transaction.GetId();
        }

        public Guid WithdrawalTransaction(Guid bankAccountId, decimal amountOfMoney)
        {
            IBankAccount bankAccount = _bankAccounts.FirstOrDefault(account => account.Id() == bankAccountId);
            if (bankAccount is null)
                throw new BanksException("bankAccount not found");
            Client client = _clients.FirstOrDefault(client1 => client1.Id == bankAccount.ClientId());
            if (client is null)
                throw new BanksException("client not found");
            UpdateSuspend(client.Id);
            if (client.IsSuspend && amountOfMoney > BankSettings.SuspendTransactionLimit)
                throw new BanksException("suspend transaction limit less than top up money");
            ITransaction transaction = new WithdrawalTransaction().Create(amountOfMoney, new List<IBankAccount>() { bankAccount });
            _transactions.Add(transaction);
            return transaction.GetId();
        }

        public Guid TransferTransaction(Guid bankAccountIdWithdrawal, Guid bankAccountIdTopUp, decimal amountOfMoney)
        {
            IBankAccount bankAccountWithdrawal = CentralBank.GetBankAccount(bankAccountIdWithdrawal);
            IBankAccount bankAccountTopUp = CentralBank.GetBankAccount(bankAccountIdTopUp);

            if (bankAccountWithdrawal is null)
                throw new BanksException("Withdrawal bankAccount not found");
            if (bankAccountTopUp is null)
                throw new BanksException("Top Up bankAccount not found");

            Client clientWithdrawal = _clients.FirstOrDefault(client1 => client1.Id == bankAccountWithdrawal.ClientId()) ?? throw new BanksException("Withdrawal client not found");
            Client clientTopUp = _clients.FirstOrDefault(client1 => client1.Id == bankAccountTopUp.ClientId()) ?? throw new BanksException("Top Up client not found");

            UpdateSuspend(clientWithdrawal.Id);
            UpdateSuspend(clientTopUp.Id);

            if (clientTopUp.IsSuspend && amountOfMoney > BankSettings.SuspendTransactionLimit)
                throw new BanksException("suspend transaction limit less than top up money");
            if (clientWithdrawal.IsSuspend && amountOfMoney > BankSettings.SuspendTransactionLimit)
                throw new BanksException("suspend transaction limit less than withdrawal money");

            ITransaction transaction = new MoneyTransferTransaction().Create(amountOfMoney, new List<IBankAccount>() { bankAccountWithdrawal, bankAccountTopUp });
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

            transaction = transaction.Cancel(CentralBank.GetBankAccountsByBankAccountsId(transaction.GetBankAccountsId()).ToList());
            return transaction.GetId();
        }

        public void WasteTimeMechanism(uint days)
        {
            foreach (IBankAccount bankAccount in _bankAccounts)
            {
                bankAccount.WasteTime(days);
            }
        }

        // public List<IBankAccount> GetBankAccounts(List<Guid> bankAccountsId)
        // {
        //     List<IBankAccount> bankAccounts = new ();
        //     foreach (Guid bankAccountId in bankAccountsId)
        //     {
        //         IBankAccount bankAccount = _bankAccounts.FirstOrDefault(account => bankAccountId == account.ClientId());
        //         bankAccount ??= CentralBank.GetBankAccount(bankAccountId) ?? throw new BanksException("BankAccount doesn't exists");
        //         bankAccounts.Add(bankAccount);
        //     }
        //
        //     return bankAccounts;
        // }
        public void CreateNotification(Notification notification)
        {
            foreach (Client client in _clients)
            {
                client.AddNotification(notification);
            }
        }

        public IBankAccount GetBankAccount(Guid bankAccountId)
        {
            return _bankAccounts.FirstOrDefault(account => account.Id() == bankAccountId);
        }

        private void UpdateSuspend(Guid clientId)
        {
            Client client = CentralBank.GetClient(clientId);
            client.UpdateClientSuspend((client.Passport == 0 && BankSettings.PassportNeeded) || (client.Address is null && BankSettings.AddressNeeded));
        }
    }
}