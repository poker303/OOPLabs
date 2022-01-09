using System;

namespace Banks.ConsoleCommands
{
    public class CancellationOfTheTransaction : CommandClass
    {
        public override void Command()
        {
            string answer = ConsoleApp.ConsoleToString("Скажите, операция происходила с одним и тем же счётом ?");

            if (answer == "Yes")
            {
                Console.WriteLine("Введите сегодняшнюю дату");
                var tempDate = Convert.ToDateTime(Console.ReadLine());

                int transactionNumber =
                    ConsoleApp.ConsoleToInt("Введите номер операции, которую хотите отменить: ");

                string bankName =
                    ConsoleApp.ConsoleToString("Введите имя банка, в котором происходила операция: ");
                Bank tempBank = FindSubtypes.FindBankByName(CentralBanks, bankName);

                tempBank.СancellationOfTheTransaction(transactionNumber);
                Console.WriteLine("Перевод успешно отменён, деньги были вернуты на счета.");
            }
            else
            {
                Console.WriteLine("Введите сегодняшнюю дату");
                var tempDate = Convert.ToDateTime(Console.ReadLine());
                int transactionNumber =
                    ConsoleApp.ConsoleToInt("Введите номер операции, которую хотите отменить: ");
                CentralBanks[0].СancellationOfTheTransaction(transactionNumber);
                Console.WriteLine("Перевод успешно отменён, деньги были вернуты на счета.");
            }
        }
    }
}