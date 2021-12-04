using System;
using System.Collections.Generic;
using Banks.AccountTypes;
using NUnit.Framework;
using Banks.Entities; 

namespace Banks.Tests
{
    public class Tests
    {
        [Test]
        public void Check()
        {
            Bank bank = CentralBank.AddBank(
                "1235",
                new BankSettings(new List<DepositPercentageRange>(), 1000, 2, 10, 1000, 123));

            Client client = bank.AddClient("name", "name");
            Client client1 = bank.AddClient("123", "123");

            bank.UpdateClientPassport(client.Id, "1234");
            bank.UpdateClientPassport(client1.Id, "1234");
            bank.CreateDebitAccount(client.Id);
            bank.CreateDebitAccount(client1.Id);

            bank.UpdateClientPassport(client.Id, "123");
            bank.UpdateClientPassport(client1.Id, "123");

            bank.UpdateClientAddress(client.Id, "123");
            bank.UpdateClientAddress(client1.Id, "123");

            Guid bankAccountGuid = bank.CreateDebitAccount(client.Id);
            Guid bankAccountGuid1 = bank.CreateDebitAccount(client1.Id);

            bank.TopUpTransaction(bankAccountGuid, 10000);
            bank.TopUpTransaction(bankAccountGuid1, 10000);

            bank.WithdrawalTransaction(bankAccountGuid, 1000);
            bank.WithdrawalTransaction(bankAccountGuid1, 1000);

            Guid transaction = bank.TransferTransaction(bankAccountGuid, bankAccountGuid1, 200);

            bank.CancelTransaction(transaction);
        }
    }
}