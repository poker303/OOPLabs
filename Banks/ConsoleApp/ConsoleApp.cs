using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Accounts;
using Banks.ConsoleSweeps;
using Banks.TransmittedParameters;

namespace Banks
{
    public class ConsoleApp
    {
        private ConsoleAppRecordingMethods _console = new ConsoleAppRecordingMethods();
        private List<string> _commands;
        private List<CentralBank> _centralBanks;
        private List<BankParameters> _templateParametersForCreatingBank;
        private List<Client> _clients;

        private CreateSubtypes _createSubtypes = new CreateSubtypes();
        private FindSubtypes _findSubtypes = new FindSubtypes();
        private OutputSubtypes _outputSubtypes = new OutputSubtypes();
        private ShowSubtypes _showSubtypes = new ShowSubtypes();

        public ConsoleApp()
        {
            _clients = new List<Client>();
            _templateParametersForCreatingBank = new List<BankParameters>();
            _centralBanks = new List<CentralBank>();
            _commands = new List<string>()
            {
                new string("CreateCentralBank"),

                new string("SettingParametersToBank"),
                new string("SettingTheAppendedAmounts"),
                new string("SettingThePercents"),
                new string("CreateBank"),

                new string("CreateClient"),
                new string("AddClientToTheBank"),
                new string("CreateAccount"),

                new string("ShowBanks"),
                new string("ShowClientAccountsInfo"),
                new string("ShowBankClients"),
                new string("ShowAccountAmountAfterTime"),

                new string("AccountWithdrawal"),
                new string("AccountReplenishment"),
                new string("Transfer"),
                new string("СancellationOfTheTransaction"),
                new string("GiveClientPermissionToSubscribe"),
                new string("SignTheClientForUpdates"),
                new string("Unsubscribe TheClientForUpdates"),
                new string("ChangeDebitAccountOptions"),
                new string("ChargeInterestAndCommissions"),
            };
        }

        public void StartConsole()
        {
            Console.WriteLine("Hello 3-hundred bucks boys!");
            while (true)
            {
                string command;
                command = Convert.ToString(Console.ReadLine());
                switch (command)
                {
                    case "CreateCentralBank":
                        string centralBankName = _console.ConsoleToString("Введите имя центрального банка");
                        string centralBankCountry = _console.ConsoleToString("Введите страну центрального банка");
                        _createSubtypes.CreateCentralBank(_centralBanks, centralBankName, centralBankCountry);
                        Console.WriteLine($"Центральный Банк: {centralBankName} в стране {centralBankCountry} создан.");
                        break;

                    case "SettingParametersToBank":
                        int number = _console.ConsoleToInt("Введите номер шаблона");
                        string nameBank = _console.ConsoleToString("Введите имя банка");
                        int numbers = _console.ConsoleToInt("Введите количество критических сумм");

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
                        int reliabilityAmount =
                            _console.ConsoleToInt("Введите ограничительную сумму если счёт ненадёжный");
                        int debitPercent = _console.ConsoleToInt("Введите процент дебетового счёта");
                        int debitCommission = _console.ConsoleToInt("Введите комиссию дебетового счёта");
                        int creditPercent = _console.ConsoleToInt("Введите процент кредитного счёта");
                        int creditCommission = _console.ConsoleToInt("Введите комиссию кредитного счёта");
                        int depositCommission = _console.ConsoleToInt("Введите комиссию депозитного счёта");
                        int creditLimit = _console.ConsoleToInt("Введите лимит по кредитным счетам счёта");

                        var parameters = new BankParameters(nameBank, table, reliabilityAmount, debitPercent, debitCommission, creditPercent, creditCommission, depositCommission, creditLimit);
                        _templateParametersForCreatingBank[number] = parameters;
                        break;

                    case "CreateBank":
                        string centralBank = _console.ConsoleToString("Введите имя центрального банка");
                        int number1 = _console.ConsoleToInt("Введите номер шаблона банка");
                        _createSubtypes.CreateBank(_templateParametersForCreatingBank, _centralBanks, centralBank, number1);
                        break;

                    case "CreateClient":
                        string clientName = _console.ConsoleToString("Введите имя клиента");
                        string clientSurname = _console.ConsoleToString("Введите фамилию клиента");
                        int clientMoney = _console.ConsoleToInt("Введите деньги клиента");

                        var client = new Client(clientName, clientSurname, clientMoney);
                        _clients.Add(client);
                        break;

                    case "AddClientToTheBank":
                        string clientName1 = _console.ConsoleToString("Введите имя клиента");
                        string clientSurName1 = _console.ConsoleToString("Введите фамилию клиента");
                        string nameBank1 = _console.ConsoleToString("Введите имя банка");

                        AddClient(clientName1, clientSurName1, nameBank1);
                        break;

                    case "CreateAccount":
                        string bankName = _console.ConsoleToString("Введите имя банка, в котором хотите создать счёт");
                        string clientName2 = _console.ConsoleToString("Введите имя клиента");
                        string clientSurName2 = _console.ConsoleToString("Введите фамилию клиента");

                        _findSubtypes.FindClientByNameSurname(_clients, clientName2, clientSurName2);
                        int amount = _console.ConsoleToInt("Введите сумму счёта");

                        Console.WriteLine("Введите дату открытия счёта.");
                        var openingDate = Convert.ToDateTime(Console.ReadLine());
                        int duration = _console.ConsoleToInt("Введите период длительности счёта.");

                        List<Client> accountOwner = _findSubtypes.FindClientByNameSurname(_clients, clientName2, clientSurName2);
                        _findSubtypes.FindBankByName(_centralBanks, bankName).CreateDebitAccount(accountOwner[0], amount, openingDate, duration);
                        break;

                    case "ShowBanks":
                        List<string> tempBanks = _showSubtypes.ShowBanks(_centralBanks);
                        Console.WriteLine("Текущие банки: ");
                        _outputSubtypes.Output(tempBanks);
                        break;

                    case "ShowClientAccountsInfo":
                        string clientName3 = _console.ConsoleToString("Введите имя клиента");
                        string clientSurName3 = _console.ConsoleToString("Введите фамилию клиента");
                        Client tempClient = _findSubtypes.FindClientByNameSurname(_clients, clientName3, clientSurName3)[0];

                        Console.WriteLine("Дебитовые счета: ");
                        foreach (DebitAccount debitAccount in tempClient.DebitAccounts)
                        {
                            Console.WriteLine(
                                $"Номер счёта: {debitAccount.Number}, Сумма на счёте: {debitAccount.Amount}");
                        }

                        Console.WriteLine("Кредитные счета: ");
                        foreach (CreditAccount creditAccount in tempClient.CreditAccounts)
                        {
                            Console.WriteLine(
                                $"Номер счёта: {creditAccount.Number}, Сумма на счёте: {creditAccount.Amount}");
                        }

                        Console.WriteLine("Дебитовые счета: ");
                        foreach (DebitAccount debitAccount in tempClient.DebitAccounts)
                        {
                            Console.WriteLine(
                                $"Номер счёта: {debitAccount.Number}, Сумма на счёте: {debitAccount.Amount}");
                        }

                        break;

                    case "ShowBankClients":
                        string bankName1 =
                            _console.ConsoleToString("Введите имя банка, клиентов которого хотите посмотреть");
                        Bank tempBank = _findSubtypes.FindBankByName(_centralBanks, bankName1);

                        List<Client> tempBankClients = _showSubtypes.ShowBankClients(tempBank);
                        Console.WriteLine("Клиенты текущего банка: ");
                        _outputSubtypes.Output(tempBankClients);
                        break;

                    case "ShowAccountAmountAfterTime":
                        int accountNumber = _console.ConsoleToInt("Введите номер счёта");

                        string bankName2 = _console.ConsoleToString("Введите имя банка, которому принадлежит счёт");
                        Bank tempBank1 = _findSubtypes.FindBankByName(_centralBanks, bankName2);

                        Console.WriteLine("Введите дату, в которую хотите посмотреть сумму на счёте");
                        var date = Convert.ToDateTime(Console.ReadLine());

                        Console.WriteLine("Сумма на счёте к введённой дате: ");
                        Console.WriteLine(_showSubtypes.ShowAccountAfterTime(_findSubtypes.FindAccountByNumber(accountNumber, tempBank1), date));
                        break;

                    case "AccountWithdrawal":
                        Console.WriteLine("Введите сегодняшнюю дату");
                        var tempDate1 = Convert.ToDateTime(Console.ReadLine());
                        int accountNumber1 = _console.ConsoleToInt("Введите номер счёта");
                        string bankName3 = _console.ConsoleToString("Введите имя банка, которому принадлежит счёт");
                        Bank tempBank2 = _findSubtypes.FindBankByName(_centralBanks, bankName3);
                        int withdrawalAmount = _console.ConsoleToInt("Введите сумму, которую хотите снять со счёта: ");

                        IAccount tempAccount1 = _findSubtypes.FindAccountByNumber(accountNumber1, tempBank2);
                        tempBank2.Withdrawal(withdrawalAmount, tempAccount1, tempDate1);
                        Console.WriteLine($"Операция прошла успешно, текущяя сумма на счёте: {tempAccount1.Amount}");
                        break;

                    case "AccountReplenishment":
                        Console.WriteLine("Введите сегодняшнюю дату");
                        var tempDate2 = Convert.ToDateTime(Console.ReadLine());
                        int accountNumber2 = _console.ConsoleToInt("Введите номер счёта");
                        string bankName4 = _console.ConsoleToString("Введите имя банка, которому принадлежит счёт");
                        Bank tempBank3 = _findSubtypes.FindBankByName(_centralBanks, bankName4);
                        int replenishmentAmount =
                            _console.ConsoleToInt("Введите сумму, которую хотите внести на счёт: ");

                        IAccount tempAccount2 = _findSubtypes.FindAccountByNumber(accountNumber2, tempBank3);
                        tempBank3.Replenishment(replenishmentAmount, tempAccount2, tempDate2);
                        Console.WriteLine($"Операция прошла успешно, текущяя сумма на счёте: {tempAccount2.Amount}");
                        break;

                    case "Transfer":
                        string answer =
                            _console.ConsoleToString(
                                "Скажите, вы хотите сделать перевод со счёта на счёт в одном банке ?");

                        if (answer == "Yes")
                        {
                            Console.WriteLine("Введите сегодняшнюю дату");
                            var tempDate3 = Convert.ToDateTime(Console.ReadLine());
                            string bankName5 =
                                _console.ConsoleToString("Введите имя банка, которому принадлежат счёта");
                            Bank tempBank4 = _findSubtypes.FindBankByName(_centralBanks, bankName5);
                            int accountNumber3 =
                                _console.ConsoleToInt("Введите номер счёта, с которого хотите перевести деньги: ");
                            IAccount tempAccount3 = _findSubtypes.FindAccountByNumber(accountNumber3, tempBank4);
                            int accountNumber4 =
                                _console.ConsoleToInt("Введите номер счёта, на который хотите перевести деньги:");
                            IAccount tempAccount4 = _findSubtypes.FindAccountByNumber(accountNumber4, tempBank4);
                            int transferAmount1 = _console.ConsoleToInt("Введите сумму, которую хотите перевести: ");

                            _centralBanks[0].Transfer(transferAmount1, tempAccount3, tempAccount4, tempBank4, tempDate3);
                            Console.WriteLine(
                                $"Операция прошла успешно, текущяя сумма на первом счёте: {tempAccount3.Amount}, на втором: {tempAccount4.Amount}");
                        }
                        else
                        {
                            Console.WriteLine("Введите сегодняшнюю дату");
                            var tempDate3 = Convert.ToDateTime(Console.ReadLine());
                            string bankName5 =
                                _console.ConsoleToString(
                                    "Введите имя банка, с аккаунта которого хотите перевести деньги");
                            Bank tempBank4 = _findSubtypes.FindBankByName(_centralBanks, bankName5);
                            string bankName6 =
                                _console.ConsoleToString(
                                    "Введите имя банка, на аккаунт которого хотите получить деньги");
                            Bank tempBank5 = _findSubtypes.FindBankByName(_centralBanks, bankName6);
                            int accountNumber3 =
                                _console.ConsoleToInt("Введите номер счёта, с которого хотите перевести деньги: ");
                            IAccount tempAccount3 = _findSubtypes.FindAccountByNumber(accountNumber3, tempBank4);
                            int accountNumber4 =
                                _console.ConsoleToInt("Введите номер счёта, на который хотите перевести деньги:");
                            IAccount tempAccount4 = _findSubtypes.FindAccountByNumber(accountNumber4, tempBank4);
                            int transferAmount1 = _console.ConsoleToInt("Введите сумму, которую хотите перевести: ");

                            _centralBanks[0].Transfer(transferAmount1, tempAccount3, tempAccount4, tempBank4, tempBank5, tempDate3);
                            Console.WriteLine(
                                $"Операция прошла успешно, текущяя сумма на первом счёте: {tempAccount3.Amount}, на втором: {tempAccount4.Amount}");
                        }

                        break;

                    case "СancellationOfTheTransaction":
                        string answer1 =
                            _console.ConsoleToString("Скажите, операция происходила с одним и тем же счётом ?");

                        if (answer1 == "Yes")
                        {
                            Console.WriteLine("Введите сегодняшнюю дату");
                            var tempDate4 = Convert.ToDateTime(Console.ReadLine());

                            int transactionNumber =
                                _console.ConsoleToInt("Введите номер операции, которую хотите отменить: ");

                            string bankName7 =
                                _console.ConsoleToString("Введите имя банка, в котором происходила операция: ");
                            Bank tempBank6 = _findSubtypes.FindBankByName(_centralBanks, bankName7);

                            tempBank6.СancellationOfTheTransaction(transactionNumber);
                            Console.WriteLine("Перевод успешно отменён, деньги были вернуты на счета.");
                        }
                        else
                        {
                            Console.WriteLine("Введите сегодняшнюю дату");
                            var tempDate4 = Convert.ToDateTime(Console.ReadLine());
                            int transactionNumber =
                                _console.ConsoleToInt("Введите номер операции, которую хотите отменить: ");
                            _centralBanks[0].СancellationOfTheTransaction(transactionNumber);
                            Console.WriteLine("Перевод успешно отменён, деньги были вернуты на счета.");
                        }

                        break;

                    case "GiveClientPermissionToSubscribe":
                        string clientName4 = _console.ConsoleToString("Введите имя клиента");
                        string clientSurName4 = _console.ConsoleToString("Введите фамилию клиента");
                        Client tempClient1 = _findSubtypes.FindClientByNameSurname(_clients, clientName4, clientSurName4)[0];
                        tempClient1.ChangeSubscriptionDesire(true);
                        break;

                    case "SignTheClientForUpdates":
                        string clientName5 = _console.ConsoleToString("Введите имя клиента");
                        string clientSurName5 = _console.ConsoleToString("Введите фамилию клиента");
                        Client tempClient2 = _findSubtypes.FindClientByNameSurname(_clients, clientName5, clientSurName5)[0];

                        string bankName8 =
                            _console.ConsoleToString(
                                "Введите имя банка, на счета которого будет подписываться клиент: ");
                        Bank tempBank7 = _findSubtypes.FindBankByName(_centralBanks, bankName8);

                        tempBank7.AttachObserver(tempClient2);
                        break;

                    case "Unsubscribe TheClientForUpdates":
                        string clientName6 = _console.ConsoleToString("Введите имя клиента");
                        string clientSurName6 = _console.ConsoleToString("Введите фамилию клиента");
                        Client tempClient3 = _findSubtypes.FindClientByNameSurname(_clients, clientName6, clientSurName6)[0];
                        string bankName9 =
                            _console.ConsoleToString(
                                "Введите имя банка, от счетов которого будет отписываться клиент: ");
                        Bank tempBank8 = _findSubtypes.FindBankByName(_centralBanks, bankName9);

                        tempBank8.DetachObserver(tempClient3);
                        break;

                    case "ChangeDebitAccountOptions":
                        string bankName10 =
                            _console.ConsoleToString(
                                "Введите имя банка, в котором будет счёт условия которого мы хотим поменять: ");
                        Bank tempBank9 = _findSubtypes.FindBankByName(_centralBanks, bankName10);

                        int accountNumber5 =
                            _console.ConsoleToInt("Введите номер дебитового счёта, условия которого хотите поменять:");
                        IAccount tempAccount5 = _findSubtypes.FindAccountByNumber(accountNumber5, tempBank9);
                        int newDebitPercent = _console.ConsoleToInt("Введите новый процент дебетового счёта");
                        int newDebitCommission = _console.ConsoleToInt("Введите новую комиссию дебетового счёта");

                        tempBank9.ChangeDebitAccountOptions(tempAccount5, newDebitPercent, newDebitCommission);
                        break;

                    case "ChargeInterestAndCommissions":
                        Console.WriteLine("Введите сегодняшнюю дату");
                        var tempDate5 = Convert.ToDateTime(Console.ReadLine());
                        _centralBanks[0].Notification(tempDate5);
                        break;
                }
            }
        }

        public void AddClient(string clientName, string clientSurName, string nameBank)
        {
            List<Client> addedClients = _findSubtypes.FindClientByNameSurname(_clients, clientName, clientSurName);

            _findSubtypes.FindBankByName(_centralBanks, nameBank).AddClients(addedClients);
        }
    }
}