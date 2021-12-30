using System;
using System.Collections.Generic;

namespace Banks.ConsoleCommands
{
    public class ShowBanks : CommandClass
    {
        public override void Command()
        {
            List<string> tempBanks = ShowSubtypes.ShowBanks(CentralBanks);
            Console.WriteLine("Текущие банки: ");
            OutputSubtypes.Output(tempBanks);
        }
    }
}