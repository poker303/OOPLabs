using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Accounts;
using Banks.ConsoleCommands;
using Banks.ConsoleSweeps;
using Banks.TransmittedParameters;

namespace Banks
{
    public class ConsoleApp
    {
        private ConsoleAppRecordingMethods _console = new ConsoleAppRecordingMethods();
        private List<string> _commands;
        private List<CentralBank> _centralBanks;
        private List<Client> _clients;
        private FindSubtypes _findSubtypes = new FindSubtypes();

        private CommandClass _createCentralBank = new CreateCentralBank();
        private CommandClass _settingParametersToBank = new SettingParametersToBank();
        private CommandClass _createBank = new CreateBank();
        private CommandClass _createClient = new CreateClient();
        private CommandClass _createAccount = new CreateAccount();
        private CommandClass _showBanks = new ShowBanks();
        private CommandClass _showClientAccountsInfo = new ShowClientAccountsInfo();
        private CommandClass _showBankClients = new ShowBankClients();
        private CommandClass _showAccountAmountAfterTime = new ShowAccountAmountAfterTime();
        private CommandClass _accountWithdrawal = new AccountWithdrawal();
        private CommandClass _accountReplenishment = new AccountReplenishment();
        private CommandClass _transfer = new Transfer();
        private CommandClass _cancellationOfTheTransaction = new CancellationOfTheTransaction();
        private CommandClass _giveClientPermissionToSubscribe = new GiveClientPermissionToSubscribe();
        private CommandClass _signTheClientForUpdates = new SignTheClientForUpdates();
        private CommandClass _unsubscribeTheClientForUpdates = new UnsubscribeTheClientForUpdates();
        private CommandClass _changeDebitAccountOptions = new ChangeDebitAccountOptions();
        private CommandClass _chargeInterestAndCommissions = new ChargeInterestAndCommissions();

        public ConsoleApp()
        {
            _clients = new List<Client>();
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
                new string("CancellationOfTheTransaction"),
                new string("GiveClientPermissionToSubscribe"),
                new string("SignTheClientForUpdates"),
                new string("UnsubscribeTheClientForUpdates"),
                new string("ChangeDebitAccountOptions"),
                new string("ChargeInterestAndCommissions"),
                new string("Stop"),
            };
        }

        public void StartConsole()
        {
            Console.WriteLine("Hello 3-hundred bucks boys!");
            while (true)
            {
                string command = Convert.ToString(Console.ReadLine());
                if (command == "Stop")
                {
                    Console.WriteLine("By boys, take care of your fingers.");
                    break;
                }

                switch (command)
                {
                    case "CreateCentralBank":
                        _createCentralBank.Command();
                        break;

                    case "SettingParametersToBank":
                        _settingParametersToBank.Command();
                        break;

                    case "CreateBank":
                        _createBank.Command();
                        break;

                    case "CreateClient":
                        _createClient.Command();
                        break;

                    case "AddClientToTheBank":
                        string clientName1 = _console.ConsoleToString("Введите имя клиента");
                        string clientSurName1 = _console.ConsoleToString("Введите фамилию клиента");
                        string nameBank1 = _console.ConsoleToString("Введите имя банка");

                        AddClient(clientName1, clientSurName1, nameBank1);
                        break;

                    case "CreateAccount":
                        _createAccount.Command();
                        break;

                    case "ShowBanks":
                        _showBanks.Command();
                        break;

                    case "ShowClientAccountsInfo":
                        _showClientAccountsInfo.Command();
                        break;

                    case "ShowBankClients":
                        _showBankClients.Command();
                        break;

                    case "ShowAccountAmountAfterTime":
                        _showAccountAmountAfterTime.Command();
                        break;

                    case "AccountWithdrawal":
                        _accountWithdrawal.Command();
                        break;

                    case "AccountReplenishment":
                        _accountReplenishment.Command();
                        break;

                    case "Transfer":
                        _transfer.Command();
                        break;

                    case "CancellationOfTheTransaction":
                        _cancellationOfTheTransaction.Command();
                        break;

                    case "GiveClientPermissionToSubscribe":
                        _giveClientPermissionToSubscribe.Command();
                        break;

                    case "SignTheClientForUpdates":
                        _signTheClientForUpdates.Command();
                        break;

                    case "UnsubscribeTheClientForUpdates":
                        _unsubscribeTheClientForUpdates.Command();
                        break;

                    case "ChangeDebitAccountOptions":
                        _changeDebitAccountOptions.Command();
                        break;

                    case "ChargeInterestAndCommissions":
                        _chargeInterestAndCommissions.Command();
                        break;
                }
            }
        }

        private void AddClient(string clientName, string clientSurName, string nameBank)
        {
            List<Client> addedClients = _findSubtypes.FindClientByNameSurname(_clients, clientName, clientSurName);

            _findSubtypes.FindBankByName(_centralBanks, nameBank).AddClients(addedClients);
        }
    }
}