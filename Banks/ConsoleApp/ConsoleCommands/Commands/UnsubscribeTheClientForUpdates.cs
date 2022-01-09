namespace Banks.ConsoleCommands
{
    public class UnsubscribeTheClientForUpdates : CommandClass
    {
        public override void Command()
        {
            string clientName = ConsoleApp.ConsoleToString("Введите имя клиента");
            string clientSurName = ConsoleApp.ConsoleToString("Введите фамилию клиента");
            Client tempClient = FindSubtypes.FindClientByNameSurname(Clients, clientName, clientSurName)[0];
            string bankName = ConsoleApp.ConsoleToString(
                    "Введите имя банка, от счетов которого будет отписываться клиент: ");
            Bank tempBank8 = FindSubtypes.FindBankByName(CentralBanks, bankName);

            tempBank8.DetachObserver(tempClient);
        }
    }
}