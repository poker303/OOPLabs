using System;

namespace Banks.ConsoleCommands
{
    public class CreateCentralBank : CommandClass
    {
        public override void Command()
        {
            string centralBankName = ConsoleApp.ConsoleToString("Введите имя центрального банка");
            string centralBankCountry = ConsoleApp.ConsoleToString("Введите страну центрального банка");
            CreateSubtypes.CreateCentralBank(CentralBanks, centralBankName, centralBankCountry);
            Console.WriteLine($"Центральный Банк: {centralBankName} в стране {centralBankCountry} создан.");
        }
    }
}