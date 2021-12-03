using System.Collections.Generic;
using Banks.Entities;
using Spectre.Console;

namespace Banks.UI
{
    public class View
    {
        // TODO: Это тыкает юзер
        // TODO: Обрабатывает пользователя
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
    }
}