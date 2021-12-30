namespace Banks.ConsoleCommands
{
    public class GiveClientPermissionToSubscribe : CommandClass
    {
        public override void Command()
        {
            string clientName = ConsoleApp.ConsoleToString("Введите имя клиента");
            string clientSurName = ConsoleApp.ConsoleToString("Введите фамилию клиента");
            Client tempClient = FindSubtypes.FindClientByNameSurname(Clients, clientName, clientSurName)[0];
            tempClient.ChangeSubscriptionDesire(true);
        }
    }
}