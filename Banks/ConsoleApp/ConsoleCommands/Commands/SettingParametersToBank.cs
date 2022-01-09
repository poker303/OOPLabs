using System;
using System.Collections.Generic;
using Banks.TransmittedParameters;

namespace Banks.ConsoleCommands
{
    public class SettingParametersToBank : CommandClass
    {
        public override void Command()
        {
            int number = ConsoleApp.ConsoleToInt("Введите номер шаблона");
            string nameBank = ConsoleApp.ConsoleToString("Введите имя банка");
            int numbers = ConsoleApp.ConsoleToInt("Введите количество критических сумм");

            var boundarySums = new List<int>();
            for (int i = 0; i < numbers; i++)
            {
                int boundarySum = Convert.ToInt32(Console.ReadLine());
                boundarySums.Add(boundarySum);
            }

            Console.WriteLine("Введите проценты");
            var percents = new List<int>();
            for (int i = 0; i < numbers - 1; i++)
            {
                int percent = Convert.ToInt32(Console.ReadLine());
                percents.Add(percent);
            }

            var table = new AmountPercentPair(boundarySums, percents);
            int reliabilityAmount = ConsoleApp.ConsoleToInt("Введите ограничительную сумму если счёт ненадёжный");
            int debitPercent = ConsoleApp.ConsoleToInt("Введите процент дебетового счёта");
            int debitCommission = ConsoleApp.ConsoleToInt("Введите комиссию дебетового счёта");
            int creditPercent = ConsoleApp.ConsoleToInt("Введите процент кредитного счёта");
            int creditCommission = ConsoleApp.ConsoleToInt("Введите комиссию кредитного счёта");
            int depositCommission = ConsoleApp.ConsoleToInt("Введите комиссию депозитного счёта");
            int creditLimit = ConsoleApp.ConsoleToInt("Введите лимит по кредитным счетам счёта");

            var parameters = new BankParameters(nameBank, table, reliabilityAmount, debitPercent, debitCommission, creditPercent, creditCommission, depositCommission, creditLimit);
            TemplateParametersForCreatingBank[number] = parameters;
        }
    }
}