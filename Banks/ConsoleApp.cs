using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Accounts;
using Banks.TransmittedParameters;

namespace Banks
{
    public class ConsoleApp
    {
        private List<string> _commands;
        private List<CentralBank> _centralBank;
        private List<BankParameters> _templateParametersForCreatingBank;
        private List<Client> _clients;
        public ConsoleApp()
        {
            _clients = new List<Client>();
            _templateParametersForCreatingBank = new List<BankParameters>();
            _centralBank = new List<CentralBank>();
            _commands = new List<string>()
            {
                new string("CreateCentralBank"),

                new string("SettingParametersToBank"),
                new string("SettingTheAppendedAmounts"),
                new string("SettingThePercents"),
                new string("CreateBank"),

                new string("CreateClient"),
                new string("AddClientToTheBank"),

                // new string("SettingParametersToAccount"),
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

                        Console.WriteLine("Введите имя центрального банка");
                        string centralBankName = Convert.ToString(Console.ReadLine());
                        Console.WriteLine("Введите страну центрального банка");
                        string centralBankCountry = Convert.ToString(Console.ReadLine());
                        CreateCentralBank(centralBankName, centralBankCountry);
                        Console.WriteLine($"Центральный Банк: {centralBankName} в стране {centralBankCountry} создан.");
                        break;

                    case "SettingParametersToBank":
                        Console.WriteLine("Введите номер шаблона");
                        int number = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Введите имя банка");
                        string nameBank = Convert.ToString(Console.ReadLine());

                        Console.WriteLine("Введите количество критических сумм");
                        int numbers = Convert.ToInt32(Console.ReadLine());
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

                        Console.WriteLine("Введите ограничительную сумму если счёт ненадёжный");
                        int reliabilityAmount = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Введите процент дебетового счёта");
                        int debitPercent = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Введите комиссию дебетового счёта");
                        int debitCommission = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Введите процент кредитного счёта");
                        int creditPercent = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Введите комиссию кредитного счёта");
                        int creditCommission = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Введите комиссию депозитного счёта");
                        int depositCommission = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Введите лимит по кредитным счетам счёта");
                        int creditLimit = Convert.ToInt32(Console.ReadLine());

                        var parameters = new BankParameters(nameBank, table, reliabilityAmount, debitPercent, debitCommission, creditPercent, creditCommission, depositCommission, creditLimit);
                        _templateParametersForCreatingBank[number] = parameters;
                        break;

                    case "CreateBank":
                        Console.WriteLine("Введите имя центрального банка");
                        string centralBank = Convert.ToString(Console.ReadLine());

                        Console.WriteLine("Введите номер шаблона банка");
                        int number1 = Convert.ToInt32(Console.ReadLine());
                        CreateBank(centralBank, number1);
                        break;

                    case "CreateClient":
                        Console.WriteLine("Введите имя клиента");
                        string clientName = Convert.ToString(Console.ReadLine());

                        Console.WriteLine("Введите фамилию банка");
                        string clientSurname = Convert.ToString(Console.ReadLine());

                        Console.WriteLine("Введите деньги клиента");
                        int clientMoney = Convert.ToInt32(Console.ReadLine());

                        var client = new Client(clientName, clientSurname, clientMoney);
                        _clients.Add(client);
                        break;

                    case "AddClientToTheBank":

                        Console.WriteLine("Введите имя клиента");
                        string clientName1 = Convert.ToString(Console.ReadLine());

                        Console.WriteLine("Введите фамилию клиента");
                        string clientSurName1 = Convert.ToString(Console.ReadLine());

                        Console.WriteLine("Введите имя банка");
                        string nameBank1 = Convert.ToString(Console.ReadLine());

                        AddClient(clientName1, clientSurName1, nameBank1);
                        break;

                    case "CreateAccount":
                        Console.WriteLine("Введите имя банка, в котором хотите создать счёт");
                        string bankName = Convert.ToString(Console.ReadLine());

                        Console.WriteLine("Введите имя клиента");
                        string clientName2 = Convert.ToString(Console.ReadLine());

                        Console.WriteLine("Введите фамилию клиента");
                        string clientSurName2 = Convert.ToString(Console.ReadLine());

                        FindClientByNameSurname(clientName2, clientSurName2);

                        Console.WriteLine("Введите сумму счёта");
                        int amount = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Введите дату открытия счёта.");
                        var openingDate = Convert.ToDateTime(Console.ReadLine());

                        Console.WriteLine("Введите период длительности счёта.");
                        int duration = Convert.ToInt32(Console.ReadLine());

                        List<Client> accountOwner = FindClientByNameSurname(clientName2, clientSurName2);
                        FindBankByName(bankName).CreateDebitAccount(accountOwner[0], amount, openingDate, duration);
                        break;

                    case "ShowBanks":
                        List<string> tempBanks = ShowBanks();
                        Console.WriteLine("Текущие банки: ");
                        Output(tempBanks);
                        break;

                    case "ShowClientAccountsInfo":
                        Console.WriteLine("Введите имя клиента");
                        string clientName3 = Convert.ToString(Console.ReadLine());
                        Console.WriteLine("Введите фамилию клиента");
                        string clientSurName3 = Convert.ToString(Console.ReadLine());
                        Client tempClient = FindClientByNameSurname(clientName3, clientSurName3)[0];

                        Console.WriteLine("Дебитовые счета: ");
                        foreach (DebitAccount debitAccount in tempClient.DebitAccounts)
                        {
                            Console.WriteLine($"Номер счёта: {debitAccount.Number}, Сумма на счёте: {debitAccount.Amount}");
                        }

                        Console.WriteLine("Кредитные счета: ");
                        foreach (CreditAccount creditAccount in tempClient.CreditAccounts)
                        {
                            Console.WriteLine($"Номер счёта: {creditAccount.Number}, Сумма на счёте: {creditAccount.Amount}");
                        }

                        Console.WriteLine("Дебитовые счета: ");
                        foreach (DebitAccount debitAccount in tempClient.DebitAccounts)
                        {
                            Console.WriteLine($"Номер счёта: {debitAccount.Number}, Сумма на счёте: {debitAccount.Amount}");
                        }

                        break;

                    case "ShowBankClients":
                        Console.WriteLine("Введите имя банка, клиентов которого хотите посмотреть");
                        string bankName1 = Convert.ToString(Console.ReadLine());
                        Bank tempBank = FindBankByName(bankName1);

                        List<Client> tempBankClients = ShowBankClients(tempBank);
                        Console.WriteLine("Клиенты текущего банка: ");
                        Output(tempBankClients);
                        break;

                    case "ShowAccountAmountAfterTime":
                        Console.WriteLine("Введите номер счёта");
                        int accountNumber = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Введите имя банка, которому принадлежит счёт");
                        string bankName2 = Convert.ToString(Console.ReadLine());
                        Bank tempBank1 = FindBankByName(bankName2);

                        Console.WriteLine("Введите дату, в которую хотите посмотреть сумму на счёте");
                        var date = Convert.ToDateTime(Console.ReadLine());

                        Console.WriteLine("Сумма на счёте к введённой дате: ");
                        Console.WriteLine(ShowAccountAfterTime(FindAccountByNumber(accountNumber, tempBank1), date));
                        break;

                    case "AccountWithdrawal":
                        Console.WriteLine("Введите сегодняшнюю дату");
                        var tempDate1 = Convert.ToDateTime(Console.ReadLine());

                        Console.WriteLine("Введите номер счёта");
                        int accountNumber1 = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Введите имя банка, которому принадлежит счёт");
                        string bankName3 = Convert.ToString(Console.ReadLine());
                        Bank tempBank2 = FindBankByName(bankName3);

                        Console.WriteLine("Введите сумму, которую хотите снять со счёта: ");
                        int withdrawalAmount = Convert.ToInt32(Console.ReadLine());

                        IAccount tempAccount1 = FindAccountByNumber(accountNumber1, tempBank2);
                        tempBank2.Withdrawal(withdrawalAmount, tempAccount1, tempDate1);
                        Console.WriteLine($"Операция прошла успешно, текущяя сумма на счёте: {tempAccount1.Amount}");
                        break;

                    case "AccountReplenishment":
                        Console.WriteLine("Введите сегодняшнюю дату");
                        var tempDate2 = Convert.ToDateTime(Console.ReadLine());

                        Console.WriteLine("Введите номер счёта");
                        int accountNumber2 = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Введите имя банка, которому принадлежит счёт");
                        string bankName4 = Convert.ToString(Console.ReadLine());
                        Bank tempBank3 = FindBankByName(bankName4);

                        Console.WriteLine("Введите сумму, которую хотите внести на счёт: ");
                        int replenishmentAmount = Convert.ToInt32(Console.ReadLine());

                        IAccount tempAccount2 = FindAccountByNumber(accountNumber2, tempBank3);
                        tempBank3.Replenishment(replenishmentAmount, tempAccount2, tempDate2);
                        Console.WriteLine($"Операция прошла успешно, текущяя сумма на счёте: {tempAccount2.Amount}");
                        break;

                    case "Transfer":
                        Console.WriteLine("Скажите, вы хотите сделать перевод со счёта на счёт в одном банке ?");
                        string answer = Convert.ToString(Console.ReadLine());

                        if (answer == "Yes")
                        {
                            Console.WriteLine("Введите сегодняшнюю дату");
                            var tempDate3 = Convert.ToDateTime(Console.ReadLine());

                            Console.WriteLine("Введите имя банка, которому принадлежат счёта");
                            string bankName5 = Convert.ToString(Console.ReadLine());
                            Bank tempBank4 = FindBankByName(bankName5);

                            Console.WriteLine("Введите номер счёта, с которого хотите перевести деньги: ");
                            int accountNumber3 = Convert.ToInt32(Console.ReadLine());
                            IAccount tempAccount3 = FindAccountByNumber(accountNumber3, tempBank4);

                            Console.WriteLine("Введите номер счёта, на который хотите перевести деньги:");
                            int accountNumber4 = Convert.ToInt32(Console.ReadLine());
                            IAccount tempAccount4 = FindAccountByNumber(accountNumber4, tempBank4);

                            Console.WriteLine("Введите сумму, которую хотите перевести: ");
                            int transferAmount1 = Convert.ToInt32(Console.ReadLine());

                            _centralBank[0].Transfer(transferAmount1, tempAccount3, tempAccount4, tempBank4, tempDate3);
                            Console.WriteLine($"Операция прошла успешно, текущяя сумма на первом счёте: {tempAccount3.Amount}, на втором: {tempAccount4.Amount}");
                        }
                        else
                        {
                            Console.WriteLine("Введите сегодняшнюю дату");
                            var tempDate3 = Convert.ToDateTime(Console.ReadLine());

                            Console.WriteLine("Введите имя банка, с аккаунта которого хотите перевести деньги");
                            string bankName5 = Convert.ToString(Console.ReadLine());
                            Bank tempBank4 = FindBankByName(bankName5);

                            Console.WriteLine("Введите имя банка, на аккаунт которого хотите получить деньги");
                            string bankName6 = Convert.ToString(Console.ReadLine());
                            Bank tempBank5 = FindBankByName(bankName6);

                            Console.WriteLine("Введите номер счёта, с которого хотите перевести деньги: ");
                            int accountNumber3 = Convert.ToInt32(Console.ReadLine());
                            IAccount tempAccount3 = FindAccountByNumber(accountNumber3, tempBank4);

                            Console.WriteLine("Введите номер счёта, на который хотите перевести деньги:");
                            int accountNumber4 = Convert.ToInt32(Console.ReadLine());
                            IAccount tempAccount4 = FindAccountByNumber(accountNumber4, tempBank4);

                            Console.WriteLine("Введите сумму, которую хотите перевести: ");
                            int transferAmount1 = Convert.ToInt32(Console.ReadLine());

                            _centralBank[0].Transfer(transferAmount1, tempAccount3, tempAccount4, tempBank4, tempBank5, tempDate3);
                            Console.WriteLine($"Операция прошла успешно, текущяя сумма на первом счёте: {tempAccount3.Amount}, на втором: {tempAccount4.Amount}");
                        }

                        break;

                    case "СancellationOfTheTransaction":
                        Console.WriteLine("Скажите, операция происходила с одним и тем же счётом ?");
                        string answer1 = Convert.ToString(Console.ReadLine());

                        if (answer1 == "Yes")
                        {
                            Console.WriteLine("Введите сегодняшнюю дату");
                            var tempDate4 = Convert.ToDateTime(Console.ReadLine());

                            Console.WriteLine("Введите номер операции, которую хотите отменить: ");
                            int transactionNumber = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("Введите имя банка, в котором происходила операция: ");
                            string bankName7 = Convert.ToString(Console.ReadLine());
                            Bank tempBank6 = FindBankByName(bankName7);

                            tempBank6.СancellationOfTheTransaction(transactionNumber, tempDate4);
                            Console.WriteLine("Перевод успешно отменён, деньги были вернуты на счета.");
                        }
                        else
                        {
                            Console.WriteLine("Введите сегодняшнюю дату");
                            var tempDate4 = Convert.ToDateTime(Console.ReadLine());

                            Console.WriteLine("Введите номер операции, которую хотите отменить: ");
                            int transactionNumber = Convert.ToInt32(Console.ReadLine());

                            _centralBank[0].СancellationOfTheTransaction(transactionNumber, tempDate4);
                            Console.WriteLine("Перевод успешно отменён, деньги были вернуты на счета.");
                        }

                        break;

                    case "GiveClientPermissionToSubscribe":
                        Console.WriteLine("Введите имя клиента");
                        string clientName4 = Convert.ToString(Console.ReadLine());
                        Console.WriteLine("Введите фамилию клиента");
                        string clientSurName4 = Convert.ToString(Console.ReadLine());
                        Client tempClient1 = FindClientByNameSurname(clientName4, clientSurName4)[0];
                        tempClient1.ChangeSubscriptionDesire(true);
                        break;

                    case "SignTheClientForUpdates":
                        Console.WriteLine("Введите имя клиента");
                        string clientName5 = Convert.ToString(Console.ReadLine());
                        Console.WriteLine("Введите фамилию клиента");
                        string clientSurName5 = Convert.ToString(Console.ReadLine());
                        Client tempClient2 = FindClientByNameSurname(clientName5, clientSurName5)[0];

                        Console.WriteLine("Введите имя банка, на счета которого будет подписываться клиент: ");
                        string bankName8 = Convert.ToString(Console.ReadLine());
                        Bank tempBank7 = FindBankByName(bankName8);

                        tempBank7.AttachObserver(tempClient2);
                        break;

                    case "Unsubscribe TheClientForUpdates":
                        Console.WriteLine("Введите имя клиента");
                        string clientName6 = Convert.ToString(Console.ReadLine());
                        Console.WriteLine("Введите фамилию клиента");
                        string clientSurName6 = Convert.ToString(Console.ReadLine());
                        Client tempClient3 = FindClientByNameSurname(clientName6, clientSurName6)[0];

                        Console.WriteLine("Введите имя банка, от счетов которого будет отписываться клиент: ");
                        string bankName9 = Convert.ToString(Console.ReadLine());
                        Bank tempBank8 = FindBankByName(bankName9);

                        tempBank8.DetachObserver(tempClient3);
                        break;

                    case "ChangeDebitAccountOptions":

                        Console.WriteLine("Введите имя банка, в котором будет счёт условия которого мы хотим поменять: ");
                        string bankName10 = Convert.ToString(Console.ReadLine());
                        Bank tempBank9 = FindBankByName(bankName10);

                        Console.WriteLine("Введите номер дебитового счёта, условия которого хотите поменять:");
                        int accountNumber5 = Convert.ToInt32(Console.ReadLine());
                        IAccount tempAccount5 = FindAccountByNumber(accountNumber5, tempBank9);

                        Console.WriteLine("Введите новый процент дебетового счёта");
                        int newDebitPercent = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Введите новую комиссию дебетового счёта");
                        int newDebitCommission = Convert.ToInt32(Console.ReadLine());

                        tempBank9.ChangeDebitAccountOptions(tempAccount5, newDebitPercent, newDebitCommission);
                        break;

                    case "ChargeInterestAndCommissions":
                        Console.WriteLine("Введите сегодняшнюю дату");
                        var tempDate5 = Convert.ToDateTime(Console.ReadLine());
                        _centralBank[0].Notification(tempDate5);
                        break;
                }
            }
        }

        public CentralBank CreateCentralBank(string centralBankName, string centralBankCountry)
        {
            var centralBank = new CentralBank(centralBankName, centralBankCountry);
            _centralBank.Add(centralBank);
            return centralBank;
        }

        public void CreateBank(string centralBankName, int bankTemplateNumber)
        {
            foreach (CentralBank cB in _centralBank.Where(cB => cB.Name == centralBankName))
            {
                cB.CreateBank(_templateParametersForCreatingBank[bankTemplateNumber]);
            }
        }

        public void AddClient(string clientName, string clientSurName, string nameBank)
        {
            List<Client> addedClients = FindClientByNameSurname(clientName, clientSurName);

            FindBankByName(nameBank).AddClients(addedClients);
        }

        public List<Client> FindClientByNameSurname(string clientName, string clientSurName)
        {
            var foundClients = _clients.Where(client => client.GetName() == clientName && client.GetSurName() == clientSurName).ToList();

            return foundClients;
        }

        public Bank FindBankByName(string nameBank)
        {
            var foundBanks = _centralBank[0].Banks.Where(bank => bank.GetName() == nameBank).ToList();

            return foundBanks[0];
        }

        public IAccount FindAccountByNumber(int number, Bank bank)
        {
            IAccount foundAccount = null;
            foreach (IAccount account in bank.CreditAccounts.Concat(bank.DebitAccounts).Concat(bank.DepositAccounts))
            {
                if (number == account.Number)
                {
                    foundAccount = account;
                }
            }

            return foundAccount;
        }

        public List<string> ShowBanks()
        {
            return _centralBank[0].Banks.Select(bank => bank.GetName()).ToList();
        }

        public void Output(List<string> outputsElements)
        {
            foreach (string element in outputsElements)
            {
                Console.WriteLine(element);
            }
        }

        public void Output(List<int> outputsElements)
        {
            foreach (int element in outputsElements)
            {
                Console.WriteLine(element);
            }
        }

        public void Output(List<Client> outputsElements)
        {
            foreach (Client element in outputsElements)
            {
                Console.WriteLine($"Имя клиента: {element.GetName()}, Фамилия клиента: {element.GetSurName()}");
            }
        }

        public List<Client> ShowBankClients(Bank bank)
        {
            return bank.Clients;
        }

        public double ShowAccountAfterTime(IAccount account, DateTime date)
        {
            var timeRewinder = new TimeRewinder("first");
            return timeRewinder.AccountAfterTime(account, date);
        }
    }
}