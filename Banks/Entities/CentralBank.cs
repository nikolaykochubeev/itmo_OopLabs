using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Interfaces;
using Banks.Tools;

namespace Banks.Entities
{
    public static class CentralBank
    {
        private static readonly List<Bank> _banks = new ();
        public static IReadOnlyList<Bank> Banks => _banks;

        public static Bank AddBank(string bankName, BankSettings bankSettings)
        {
            var bank = new Bank(bankName, bankSettings);
            _banks.Add(bank);
            return bank;
        }

        public static Bank GetBank(Guid bankId)
        {
            return _banks.FirstOrDefault(bank => bank.BankId == bankId);
        }

        public static Client GetClient(Guid clientId)
        {
            return (from bank in _banks where bank.GetClient(clientId) is not null select bank.GetClient(clientId)).FirstOrDefault();
        }

        public static IBankAccount GetBankAccount(Guid bankAccountId)
        {
            return _banks.SelectMany(bank => bank.BankAccounts).FirstOrDefault(account => account.Id() == bankAccountId);
        }

        public static IEnumerable<IBankAccount> GetBankAccountsByBankAccountsId(IEnumerable<Guid> bankAccountId)
        {
            return _banks.SelectMany(bank => bank.BankAccounts).Where(account => bankAccountId.Contains(account.Id()));
        }

        public static void StartWasteTime(uint days)
        {
            foreach (Bank bank in _banks)
            {
                bank.WasteTimeMechanism(days);
            }
        }
    }
}