using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Banks.AccountTypes;
using Banks.Entities;
using Banks.Interfaces;
using Banks.UI;

namespace Banks
{
    internal static class Program
    {
        private static void Main()
        {
            var view = new View();
            view.InitMainMenu();

            // while (true)
            // {
            //     Console.WriteLine("AddBank, AddClientToBank, AddBankAccount, CreateTransaction, CancelTransaction");
            //     string command = Console.ReadLine();
            //     switch (command)
            //     {
            //         case "AddBank":
            //             throw new NotImplementedException();
            //             break;
            //         case "AddClientToBank":
            //             Console.WriteLine("Enter bank id");
            //             Guid bankId = Guid.Parse(Console.ReadLine() ?? string.Empty);
            //             Bank bank = CentralBank.GetBank(bankId);
            //
            //             Console.WriteLine("Enter firstname");
            //             string firstName = Console.ReadLine();
            //
            //             Console.WriteLine("Enter lastname");
            //             string lastName = Console.ReadLine();
            //
            //             bank.AddClient(firstName, lastName);
            //
            //             Console.WriteLine("Enter passport or leave this line by running");
            //             string passport = Console.ReadLine();
            //
            //             Console.WriteLine("Enter address or leave this line by running");
            //
            //             break;
            //         case "AddBankAccount":
            //             var guid = Guid.Parse(Console.ReadLine());
            //             break;
            //         case "CreateTransaction":
            //             throw new NotImplementedException();
            //             break;
            //         case "CancelTransaction":
            //             throw new NotImplementedException();
            //             break;
            //         default:
            //             Console.WriteLine("Unknown command");
            //             break;
            //     }
            // }
        }
    }
}
