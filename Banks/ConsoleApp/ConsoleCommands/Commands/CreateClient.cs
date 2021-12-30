namespace Banks.ConsoleCommands
{
    public class CreateClient : CommandClass
    {
        public override void Command()
        {
            string clientName = ConsoleApp.ConsoleToString("Введите имя клиента");
            string clientSurname = ConsoleApp.ConsoleToString("Введите фамилию клиента");
            int clientMoney = ConsoleApp.ConsoleToInt("Введите деньги клиента");

            var client = new Client(clientName, clientSurname, clientMoney);
            Clients.Add(client);
        }
    }
}