using System;
using Banks.Accounts;

namespace Banks.ConsoleCommands
{
    public class ShowClientAccountsInfo : CommandClass
    {
        public override void Command()
        {
            string clientName = ConsoleApp.ConsoleToString("Введите имя клиента");
            string clientSurName = ConsoleApp.ConsoleToString("Введите фамилию клиента");
            Client tempClient = FindSubtypes.FindClientByNameSurname(Clients, clientName, clientSurName)[0];

            Console.WriteLine("Дебитовые счета: ");
            foreach (DebitAccount debitAccount in tempClient.DebitAccounts)
            {
                Console.WriteLine(
                    $"Номер счёта: {debitAccount.Number}, Сумма на счёте: {debitAccount.Amount}");
            }

            Console.WriteLine("Кредитные счета: ");
            foreach (CreditAccount creditAccount in tempClient.CreditAccounts)
            {
                Console.WriteLine(
                    $"Номер счёта: {creditAccount.Number}, Сумма на счёте: {creditAccount.Amount}");
            }

            Console.WriteLine("Дебитовые счета: ");
            foreach (DebitAccount debitAccount in tempClient.DebitAccounts)
            {
                Console.WriteLine(
                    $"Номер счёта: {debitAccount.Number}, Сумма на счёте: {debitAccount.Amount}");
            }
        }
    }
}