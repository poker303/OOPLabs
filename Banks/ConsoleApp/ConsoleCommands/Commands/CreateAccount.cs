using System;
using System.Collections.Generic;

namespace Banks.ConsoleCommands
{
    public class CreateAccount : CommandClass
    {
        public override void Command()
        {
            string bankName = ConsoleApp.ConsoleToString("Введите имя банка, в котором хотите создать счёт");
            string clientName = ConsoleApp.ConsoleToString("Введите имя клиента");
            string clientSurName = ConsoleApp.ConsoleToString("Введите фамилию клиента");

            FindSubtypes.FindClientByNameSurname(Clients, clientName, clientSurName);
            int amount = ConsoleApp.ConsoleToInt("Введите сумму счёта");

            Console.WriteLine("Введите дату открытия счёта.");
            var openingDate = Convert.ToDateTime(Console.ReadLine());
            int duration = ConsoleApp.ConsoleToInt("Введите период длительности счёта.");

            List<Client> accountOwner = FindSubtypes.FindClientByNameSurname(Clients, clientName, clientSurName);
            FindSubtypes.FindBankByName(CentralBanks, bankName).CreateDebitAccount(accountOwner[0], amount, openingDate, duration);
        }
    }
}