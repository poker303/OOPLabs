using System;
using Banks.Accounts;

namespace Banks.ConsoleCommands
{
    public class AccountWithdrawal : CommandClass
    {
        public override void Command()
        {
            Console.WriteLine("Введите сегодняшнюю дату");
            var tempDate = Convert.ToDateTime(Console.ReadLine());
            int accountNumber = ConsoleApp.ConsoleToInt("Введите номер счёта");
            string bankName = ConsoleApp.ConsoleToString("Введите имя банка, которому принадлежит счёт");
            Bank tempBank = FindSubtypes.FindBankByName(CentralBanks, bankName);
            int withdrawalAmount = ConsoleApp.ConsoleToInt("Введите сумму, которую хотите снять со счёта: ");

            IAccount tempAccount = FindSubtypes.FindAccountByNumber(accountNumber, tempBank);
            tempBank.Withdrawal(withdrawalAmount, tempAccount, tempDate);
            Console.WriteLine($"Операция прошла успешно, текущяя сумма на счёте: {tempAccount.Amount}");
        }
    }
}