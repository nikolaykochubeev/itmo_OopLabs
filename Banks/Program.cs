using System;
using System.Collections.Generic;
using System.Linq;
using Banks.AccountTypes;
using Banks.Entities;
using Banks.Interfaces;

namespace Banks
{
    internal static class Program
    {
        private static void Main()
        {
            Bank bank = CentralBank.AddBank(
                "1235",
                new BankSettings(new List<DepositPercentageRange>(), true, true, 1000, 2, 10, 1000, 123));

            Client client = bank.AddClient("name", "name");
            Client client1 = bank.AddClient("123", "123");

            bank.UpdateClientPassport(client.Id, 1234);
            bank.UpdateClientPassport(client1.Id, 1234);
            bank.CreateDebitAccount(client.Id);
            bank.CreateDebitAccount(client1.Id);

            bank.UpdateClientPassport(client.Id, 123);
            bank.UpdateClientPassport(client1.Id, 123);

            bank.UpdateClientAddress(client.Id, "123");
            bank.UpdateClientAddress(client1.Id, "123");

            Guid bankAccountGuid = bank.CreateDebitAccount(client.Id);
            Guid bankAccountGuid1 = bank.CreateDebitAccount(client1.Id);

            bank.TopUpTransaction(bankAccountGuid, 10000);
            bank.TopUpTransaction(bankAccountGuid1, 10000);

            Console.WriteLine(bank.GetBankAccount(bankAccountGuid).AmountOfMoney());
            Console.WriteLine(bank.GetBankAccount(bankAccountGuid1).AmountOfMoney());

            bank.WithdrawalTransaction(bankAccountGuid, 1000);
            bank.WithdrawalTransaction(bankAccountGuid1, 1000);

            Console.WriteLine(bank.GetBankAccount(bankAccountGuid).AmountOfMoney());
            Console.WriteLine(bank.GetBankAccount(bankAccountGuid).AmountOfMoney());

            Guid transaction = bank.TransferTransaction(bankAccountGuid, bankAccountGuid1, 200);

            Console.WriteLine(bank.GetBankAccount(bankAccountGuid).AmountOfMoney());
            Console.WriteLine(bank.GetBankAccount(bankAccountGuid1).AmountOfMoney());

            bank.CancelTransaction(transaction);

            Console.WriteLine(bank.GetBankAccount(bankAccountGuid).AmountOfMoney());
            Console.WriteLine(bank.GetBankAccount(bankAccountGuid1).AmountOfMoney());
        }
    }
}
