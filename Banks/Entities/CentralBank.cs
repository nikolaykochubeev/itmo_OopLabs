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

        public static Bank AddBank(BankSettings bankSettings)
        {
            var bank = new Bank(bankSettings);
            _banks.Add(bank);
            return bank;
        }

        public static IBankAccount GetBankAccount(Guid bankAccountId)
        {
            return _banks.SelectMany(bank => bank.BankAccounts).FirstOrDefault(account => account.Id() == bankAccountId);
        }

        public static IEnumerable<IBankAccount> GetBankAccounts(IEnumerable<Guid> bankAccountId)
        {
            return _banks.SelectMany(bank => bank.BankAccounts).Where(account => bankAccountId.Contains(account.Id()));
        }
    }
}