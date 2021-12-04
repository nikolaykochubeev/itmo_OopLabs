using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Banks.AccountTypes;
using Banks.Entities;
using Banks.Interfaces;
using Banks.Tools;
using Banks.UI;

namespace Banks
{
    internal static class Program
    {
        private static void Main()
        {
            var view = new View(new ViewModel());
            while (true)
            {
                try
                {
                    Console.WriteLine("\n" + "AddBank, AddClientToBank, AddBankAccount, CreateTransaction, CancelTransaction" + "Get client info" + "\n");
                    string command = Console.ReadLine();
                    switch (command)
                    {
                        case "AddBank":
                            Console.WriteLine("Enter bank name");
                            string bankName = Console.ReadLine();

                            Console.WriteLine("Is passport needed? 1 or 0");
                            break;

                        case "AddClientToBank":
                            Console.WriteLine("Enter bank id");
                            var bankId = Guid.Parse(Console.ReadLine() ?? string.Empty);
                            Bank bank = CentralBank.GetBank(bankId);
                            if (bank is null)
                                throw new BanksException("Bank not found");

                            Console.WriteLine("Enter firstname");
                            string firstName = Console.ReadLine();

                            Console.WriteLine("Enter lastname");
                            string lastName = Console.ReadLine();

                            Client client = bank.AddClient(firstName, lastName);

                            Console.WriteLine("Enter passport or leave this line by running");
                            string passport = Console.ReadLine();
                            bank.UpdateClientPassport(client.Id, passport);

                            Console.WriteLine("Enter address or leave this line by running");
                            string address = Console.ReadLine();
                            bank.UpdateClientAddress(client.Id, address);
                            Console.WriteLine("Client was successfully created" + "\n" + "Client id: " + client.Id.ToString());
                            break;

                        case "AddBankAccount":
                            Console.WriteLine("Enter bank id");
                            bankId = Guid.Parse(Console.ReadLine());
                            bank = CentralBank.GetBank(bankId);

                            Console.WriteLine("Enter client account id");
                            var clientId = Guid.Parse(Console.ReadLine());
                            client = bank.GetClient(clientId);

                            Console.WriteLine("Enter account type. debit, deposit or credit");
                            string accountType = Console.ReadLine();
                            Guid bankAccountId = accountType.ToLower() switch
                            {
                                "debit" => bank.CreateDebitAccount(clientId),
                                "deposit" => bank.CreateDepositAccount(clientId),
                                "credit" => bank.CreateCreditAccount(clientId),
                                _ => throw new BanksException("this type of account does not exist")
                            };

                            Console.WriteLine("Bank account was successfully created" + "\n" + "BankAccount id: " + bankAccountId);
                            break;

                        case "CreateTransaction":
                            Console.WriteLine("Enter bank id");
                            bankId = Guid.Parse(Console.ReadLine());
                            bank = CentralBank.GetBank(bankId);

                            Console.WriteLine("Enter bank account id");
                            bankAccountId = Guid.Parse(Console.ReadLine());

                            Console.WriteLine("Enter transaction type. top up, withdrawal, transfer");
                            string transactionType = Console.ReadLine();

                            Guid transactionId;
                            decimal amountOfMoney;
                            switch (transactionType.ToLower())
                            {
                                case "top up":
                                    Console.WriteLine("Enter amount of money");
                                    amountOfMoney = Convert.ToDecimal(Console.ReadLine());

                                    transactionId = bank.TopUpTransaction(bankAccountId, amountOfMoney);
                                    break;

                                case "withdrawal":
                                    Console.WriteLine("Enter amount of money");
                                    amountOfMoney = Convert.ToDecimal(Console.ReadLine());

                                    transactionId = bank.WithdrawalTransaction(bankAccountId, amountOfMoney);
                                    break;

                                case "transfer":
                                    Console.WriteLine("Enter second bank account id to whose account you want to transfer money");
                                    var secondBankAccountId = Guid.Parse(Console.ReadLine());

                                    Console.WriteLine("Enter amount of money");
                                    amountOfMoney = Convert.ToDecimal(Console.ReadLine());

                                    transactionId = bank.TransferTransaction(bankAccountId, secondBankAccountId, amountOfMoney);
                                    break;

                                default:
                                    throw new BanksException("this type of transaction does not exist");
                            }

                            Console.WriteLine("Transaction was successfully created" + "\n" + "Transaction id: " + transactionId);
                            break;

                        case "CancelTransaction":
                            Console.WriteLine("Enter transaction id");
                            transactionId = Guid.Parse(Console.ReadLine());
                            bank = CentralBank.GetBankByTransactionId(transactionId);

                            if (bank is null)
                                throw new BanksException("bank does not exists");
                            bank.CancelTransaction(transactionId);
                            Console.WriteLine("Transaction was successfylly canceled");
                            break;

                        default:
                            Console.WriteLine("Unknown command");
                            break;

                        case "Get client info":
                            Console.WriteLine("Enter client id");
                            clientId = Guid.Parse(Console.ReadLine());
                            client = CentralBank.GetClient(clientId);
                            Console.WriteLine(client.ToString());
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
