using System;
using Banks.Accounts;

namespace Banks.ConsoleCommands
{
    public class AccountReplenishment : CommandClass
    {
        public override void Command()
        {
            Console.WriteLine("Введите сегодняшнюю дату");
            var tempDate = Convert.ToDateTime(Console.ReadLine());
            int accountNumber = ConsoleApp.ConsoleToInt("Введите номер счёта");
            string bankName = ConsoleApp.ConsoleToString("Введите имя банка, которому принадлежит счёт");
            Bank tempBank = FindSubtypes.FindBankByName(CentralBanks, bankName);
            int replenishmentAmount = ConsoleApp.ConsoleToInt("Введите сумму, которую хотите внести на счёт: ");

            IAccount tempAccount = FindSubtypes.FindAccountByNumber(accountNumber, tempBank);
            tempBank.Replenishment(replenishmentAmount, tempAccount, tempDate);
            Console.WriteLine($"Операция прошла успешно, текущяя сумма на счёте: {tempAccount.Amount}");
        }
    }
}