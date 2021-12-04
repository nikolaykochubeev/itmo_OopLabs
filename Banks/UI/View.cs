using System.Collections.Generic;
using Banks.Entities;
using Spectre.Console;

// Здесь должен был быть нормальный UI...
namespace Banks.UI
{
    public class View
    {
        private ViewModel _viewModel;

        public View(ViewModel viewModel)
        {
            this._viewModel = viewModel;
        }

        public string InitMainMenu()
        {
            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Menu")
                    .PageSize(10)
                    .AddChoices(
                        "Add bank",
                        "Change bank settings",
                        "Add client",
                        "Create new bank account",
                        "Make transaction",
                        "Get account transactions information",
                        "Get account information",
                        "Exit"));
            AnsiConsole.Clear();
            return choice;
        }

        public void AddBankSettings()
        {
            // Ask for the user's favorite fruits
            List<string> settings = AnsiConsole.Prompt(
                new MultiSelectionPrompt<string>()
                    .Title("AddBankSettings")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal settings)[/]")
                    .InstructionsText(
                        "[grey](Press [blue]<space>[/] to toggle, " +
                        "[green]<enter>[/] to accept)[/]")
                    .AddChoices(new[]
                    {
                        "Apple", "Apricot", "Avocado",
                        "Banana", "Blackcurrant", "Blueberry",
                        "Cherry", "Cloudberry", "Cocunut",
                    }));

            foreach (string setting in settings)
            {
                AnsiConsole.WriteLine(setting);
            }

            string s = AnsiConsole.Ask<string>("123");
            AnsiConsole.WriteLine(s);
        }
    }
}