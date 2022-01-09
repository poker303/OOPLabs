using System;

namespace Banks.ConsoleCommands
{
    public class ChargeInterestAndCommissions : CommandClass
    {
        public override void Command()
        {
            Console.WriteLine("Введите сегодняшнюю дату");
            var tempDate = Convert.ToDateTime(Console.ReadLine());
            CentralBanks[0].Notification(tempDate);
        }
    }
}