using System;
using System.Collections.Generic;

namespace Banks.ConsoleCommands
{
    public class ShowBankClients : CommandClass
    {
        public override void Command()
        {
            string bankName = ConsoleApp.ConsoleToString("Введите имя банка, клиентов которого хотите посмотреть");
            Bank tempBank = FindSubtypes.FindBankByName(CentralBanks, bankName);

            List<Client> tempBankClients = ShowSubtypes.ShowBankClients(tempBank);
            Console.WriteLine("Клиенты текущего банка: ");
            OutputSubtypes.Output(tempBankClients);
        }
    }
}