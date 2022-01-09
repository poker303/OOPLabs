namespace Banks.ConsoleCommands
{
    public class CreateBank : CommandClass
    {
        public override void Command()
        {
            string centralBank = ConsoleApp.ConsoleToString("Введите имя центрального банка");
            int number = ConsoleApp.ConsoleToInt("Введите номер шаблона банка");
            CreateSubtypes.CreateBank(TemplateParametersForCreatingBank, CentralBanks, centralBank, number);
        }
    }
}