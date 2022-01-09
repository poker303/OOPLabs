using System;
using Banks.Accounts;

namespace Banks.ConsoleCommands
{
    public class Transfer : CommandClass
    {
        public override void Command()
        {
            string answer =
                ConsoleApp.ConsoleToString("Скажите, вы хотите сделать перевод со счёта на счёт в одном банке ?");

            if (answer == "Yes")
            {
                Console.WriteLine("Введите сегодняшнюю дату");
                var tempDate = Convert.ToDateTime(Console.ReadLine());
                string bankName =
                    ConsoleApp.ConsoleToString("Введите имя банка, которому принадлежат счёта");
                Bank tempBank = FindSubtypes.FindBankByName(CentralBanks, bankName);
                int accountNumber1 =
                    ConsoleApp.ConsoleToInt("Введите номер счёта, с которого хотите перевести деньги: ");
                IAccount tempAccount3 = FindSubtypes.FindAccountByNumber(accountNumber1, tempBank);
                int accountNumber2 =
                    ConsoleApp.ConsoleToInt("Введите номер счёта, на который хотите перевести деньги:");
                IAccount tempAccount = FindSubtypes.FindAccountByNumber(accountNumber2, tempBank);
                int transferAmount = ConsoleApp.ConsoleToInt("Введите сумму, которую хотите перевести: ");

                CentralBanks[0].Transfer(transferAmount, tempAccount3, tempAccount, tempBank, tempDate);
                Console.WriteLine(
                    $"Операция прошла успешно, текущяя сумма на первом счёте: {tempAccount3.Amount}, на втором: {tempAccount.Amount}");
            }
            else
            {
                Console.WriteLine("Введите сегодняшнюю дату");
                var tempDate = Convert.ToDateTime(Console.ReadLine());
                string bankName1 =
                    ConsoleApp.ConsoleToString(
                        "Введите имя банка, с аккаунта которого хотите перевести деньги");
                Bank tempBank1 = FindSubtypes.FindBankByName(CentralBanks, bankName1);
                string bankName2 =
                    ConsoleApp.ConsoleToString(
                        "Введите имя банка, на аккаунт которого хотите получить деньги");
                Bank tempBank2 = FindSubtypes.FindBankByName(CentralBanks, bankName2);
                int accountNumber1 =
                    ConsoleApp.ConsoleToInt("Введите номер счёта, с которого хотите перевести деньги: ");
                IAccount tempAccount1 = FindSubtypes.FindAccountByNumber(accountNumber1, tempBank1);
                int accountNumber2 =
                    ConsoleApp.ConsoleToInt("Введите номер счёта, на который хотите перевести деньги:");
                IAccount tempAccount2 = FindSubtypes.FindAccountByNumber(accountNumber2, tempBank1);
                int transferAmount = ConsoleApp.ConsoleToInt("Введите сумму, которую хотите перевести: ");

                CentralBanks[0].Transfer(transferAmount, tempAccount1, tempAccount2, tempBank1, tempBank2, tempDate);
                Console.WriteLine(
                    $"Операция прошла успешно, текущяя сумма на первом счёте: {tempAccount1.Amount}, на втором: {tempAccount2.Amount}");
            }
        }
    }
}