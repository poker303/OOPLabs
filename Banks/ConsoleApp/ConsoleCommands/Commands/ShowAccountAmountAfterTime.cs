using System;

namespace Banks.ConsoleCommands
{
    public class ShowAccountAmountAfterTime : CommandClass
    {
        public override void Command()
        {
            int accountNumber = ConsoleApp.ConsoleToInt("Введите номер счёта");

            string bankName = ConsoleApp.ConsoleToString("Введите имя банка, которому принадлежит счёт");
            Bank tempBank = FindSubtypes.FindBankByName(CentralBanks, bankName);

            Console.WriteLine("Введите дату, в которую хотите посмотреть сумму на счёте");
            var date = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Сумма на счёте к введённой дате: ");
            Console.WriteLine(ShowSubtypes.ShowAccountAfterTime(FindSubtypes.FindAccountByNumber(accountNumber, tempBank), date));
        }
    }
}