using Banks.Accounts;

namespace Banks.ConsoleCommands
{
    public class ChangeDebitAccountOptions : CommandClass
    {
        public override void Command()
        {
            string bankName = ConsoleApp.ConsoleToString(
                    "Введите имя банка, в котором будет счёт условия которого мы хотим поменять: ");
            Bank tempBank = FindSubtypes.FindBankByName(CentralBanks, bankName);

            int accountNumber =
                ConsoleApp.ConsoleToInt("Введите номер дебитового счёта, условия которого хотите поменять:");
            IAccount tempAccount = FindSubtypes.FindAccountByNumber(accountNumber, tempBank);
            int newDebitPercent = ConsoleApp.ConsoleToInt("Введите новый процент дебетового счёта");
            int newDebitCommission = ConsoleApp.ConsoleToInt("Введите новую комиссию дебетового счёта");

            tempBank.ChangeDebitAccountOptions(tempAccount, newDebitPercent, newDebitCommission);
        }
    }
}