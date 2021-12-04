using System;
using System.Collections.Generic;
using Banks.AccountTypes;
using Banks.Entities;
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
                    Console.WriteLine("\n" + "AddBank, AddClientToBank, AddBankAccount, CreateTransaction, CancelTransaction, GetClientInfo, StartWasteTime" + "\n");
                    string command = Console.ReadLine();
                    switch (command)
                    {
                        case "AddBank":
                            Console.WriteLine("Enter bank name");
                            string bankName = Console.ReadLine();
                            var depositAnnualPercentages = new List<DepositPercentageRange>();

                            Console.WriteLine("\n" + "Enter depositAnnualPercentages" + "\n");
                            while (true)
                            {
                                Console.WriteLine("Enter from");
                                decimal from = Convert.ToDecimal(Console.ReadLine());

                                Console.WriteLine("Enter before");
                                decimal before = Convert.ToDecimal(Console.ReadLine());
                                if (from > before)
                                {
                                    Console.WriteLine("from cannot be more than before");
                                    continue;
                                }

                                Console.WriteLine("Enter percentage");
                                decimal percentage = Convert.ToDecimal(Console.ReadLine());

                                depositAnnualPercentages.Add(new DepositPercentageRange(from, before, percentage));
                                Console.WriteLine("Do you want to enter another range. yes/no");
                                if (Console.ReadLine() is "no")
                                {
                                    break;
                                }
                            }

                            Console.WriteLine("\n" + "Enter suspendTransactionLimit" + "\n");
                            decimal suspendTransactionLimit = Convert.ToDecimal(Console.ReadLine());

                            Console.WriteLine("Enter debitAnnualPercentage" + "\n");
                            decimal debitAnnualPercentage = Convert.ToDecimal(Console.ReadLine());

                            Console.WriteLine("Enter creditAnnualPercentage" + "\n");
                            decimal creditAnnualPercentage = Convert.ToDecimal(Console.ReadLine());

                            Console.WriteLine("Enter creditWithdrawalLimit" + "\n");
                            decimal creditWithdrawalLimit = Convert.ToDecimal(Console.ReadLine());

                            Console.WriteLine("Enter depositAccountExpirationDate" + "\n");
                            uint depositAccountExpirationDate = Convert.ToUInt32(Console.ReadLine());

                            var bankSettings = new BankSettings(
                                depositAnnualPercentages,
                                suspendTransactionLimit,
                                debitAnnualPercentage,
                                creditAnnualPercentage,
                                creditWithdrawalLimit,
                                depositAccountExpirationDate);
                            Bank bank = CentralBank.AddBank(bankName, bankSettings);
                            Console.WriteLine("Bank was successfully created" + "\n" + "Bank id: " + bank.BankId);
                            break;

                        case "AddClientToBank":
                            Console.WriteLine("Enter bank id" + "\n");
                            var bankId = Guid.Parse(Console.ReadLine() ?? string.Empty);
                            bank = CentralBank.GetBank(bankId);
                            if (bank is null)
                                throw new BanksException("Bank not found" + "\n");

                            Console.WriteLine("Enter firstname" + "\n");
                            string firstName = Console.ReadLine();

                            Console.WriteLine("Enter lastname" + "\n");
                            string lastName = Console.ReadLine();

                            Client client = bank.AddClient(firstName, lastName);

                            Console.WriteLine("Enter passport or leave this line by running" + "\n");
                            string passport = Console.ReadLine();
                            bank.UpdateClientPassport(client.Id, passport);

                            Console.WriteLine("Enter address or leave this line by running" + "\n");
                            string address = Console.ReadLine();
                            bank.UpdateClientAddress(client.Id, address);
                            Console.WriteLine("Client was successfully created" + "\n" + "Client id: " + client.Id.ToString());
                            break;

                        case "AddBankAccount":
                            Console.WriteLine("Enter bank id" + "\n");
                            bankId = Guid.Parse(Console.ReadLine());
                            bank = CentralBank.GetBank(bankId);

                            Console.WriteLine("Enter client account id" + "\n");
                            var clientId = Guid.Parse(Console.ReadLine());
                            client = bank.GetClient(clientId);

                            Console.WriteLine("Enter account type. debit, deposit or credit" + "\n");
                            string accountType = Console.ReadLine();
                            Guid bankAccountId = accountType.ToLower() switch
                            {
                                "debit" => bank.CreateDebitAccount(clientId),
                                "deposit" => bank.CreateDepositAccount(clientId),
                                "credit" => bank.CreateCreditAccount(clientId),
                                _ => throw new BanksException("this type of account does not exist" + "\n")
                            };

                            Console.WriteLine("Bank account was successfully created" + "\n" + "BankAccount id: " + bankAccountId);
                            break;

                        case "CreateTransaction":
                            Console.WriteLine("Enter bank id" + "\n");
                            bankId = Guid.Parse(Console.ReadLine());
                            bank = CentralBank.GetBank(bankId);

                            Console.WriteLine("Enter bank account id" + "\n");
                            bankAccountId = Guid.Parse(Console.ReadLine());

                            Console.WriteLine("Enter transaction type. top up, withdrawal, transfer" + "\n");
                            string transactionType = Console.ReadLine();

                            Guid transactionId;
                            decimal amountOfMoney;
                            switch (transactionType.ToLower())
                            {
                                case "top up":
                                    Console.WriteLine("Enter amount of money" + "\n");
                                    amountOfMoney = Convert.ToDecimal(Console.ReadLine());

                                    transactionId = bank.TopUpTransaction(bankAccountId, amountOfMoney);
                                    break;

                                case "withdrawal":
                                    Console.WriteLine("Enter amount of money" + "\n");
                                    amountOfMoney = Convert.ToDecimal(Console.ReadLine());

                                    transactionId = bank.WithdrawalTransaction(bankAccountId, amountOfMoney);
                                    break;

                                case "transfer":
                                    Console.WriteLine("Enter second bank account id to whose account you want to transfer money" + "\n");
                                    var secondBankAccountId = Guid.Parse(Console.ReadLine());

                                    Console.WriteLine("Enter amount of money" + "\n");
                                    amountOfMoney = Convert.ToDecimal(Console.ReadLine());

                                    transactionId = bank.TransferTransaction(bankAccountId, secondBankAccountId, amountOfMoney);
                                    break;

                                default:
                                    throw new BanksException("this type of transaction does not exist" + "\n");
                            }

                            Console.WriteLine("Transaction was successfully created" + "\n" + "Transaction id: " + transactionId);
                            break;

                        case "CancelTransaction":
                            Console.WriteLine("Enter transaction id" + "\n");
                            transactionId = Guid.Parse(Console.ReadLine());
                            bank = CentralBank.GetBankByTransactionId(transactionId);

                            if (bank is null)
                                throw new BanksException("bank does not exists" + "\n");
                            bank.CancelTransaction(transactionId);
                            Console.WriteLine("Transaction was successfully canceled" + "\n");
                            break;

                        default:
                            Console.WriteLine("Unknown command " + "\n");
                            break;

                        case "GetClientInfo":
                            Console.WriteLine("Enter client id" + "\n");
                            clientId = Guid.Parse(Console.ReadLine());
                            client = CentralBank.GetClient(clientId);
                            Console.WriteLine(client.ToString());
                            break;
                        case "StartWasteTime":
                            Console.WriteLine("Enter the number of days" + "\n");
                            uint days = Convert.ToUInt32(Console.ReadLine());
                            CentralBank.StartWasteTime(days);
                            Console.WriteLine("Waste Time successfully completed" + "\n");
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
