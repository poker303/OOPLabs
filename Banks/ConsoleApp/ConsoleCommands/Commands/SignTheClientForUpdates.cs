namespace Banks.ConsoleCommands
{
    public class SignTheClientForUpdates : CommandClass
    {
        public override void Command()
        {
            string clientName = ConsoleApp.ConsoleToString("Введите имя клиента");
            string clientSurName = ConsoleApp.ConsoleToString("Введите фамилию клиента");
            Client tempClient = FindSubtypes.FindClientByNameSurname(Clients, clientName, clientSurName)[0];

            string bankName8 = ConsoleApp.ConsoleToString(
                    "Введите имя банка, на счета которого будет подписываться клиент: ");
            Bank tempBank7 = FindSubtypes.FindBankByName(CentralBanks, bankName8);

            tempBank7.AttachObserver(tempClient);
        }
    }
}