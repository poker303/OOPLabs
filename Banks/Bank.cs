using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Accounts;

namespace Banks
{
    public class Bank
    {
        private int _accountNumbers = 1;

        // private int creditNumber = 1;
        // private int depositNumber = 1;
        // private int debitNumber = 1;
        public Bank(string name, List<AmountPercentPair> table)
        {
            Name = name;
            Table = table;
            CreditAccounts = new List<CreditAccount>();
            DepositAccounts = new List<DepositAccount>();
            DebitAccounts = new List<DebitAccount>();
            Transactions = new List<Transaction>();
        }

        public List<AmountPercentPair> Table { get; set; }

        public string Name { get; set; }

        public List<Client> Clients { get; set; }
        public List<Transaction> Transactions { get; set; }

        public List<CreditAccount> CreditAccounts { get; set; }
        public List<DepositAccount> DepositAccounts { get; set; }
        public List<DebitAccount> DebitAccounts { get; set; }

        public List<Client> AddClients(List<Client> clients)
        {
            foreach (Client client in Clients.Where(client => !Clients.Contains(client)))
            {
                Clients.Add(client);
            }

            // ??return clients
            return Clients;
        }

        public DebitAccount CreateDebitAccount(Client client, int percent, int initialAmount)
        {
            var newDebitAccount = new DebitAccount(_accountNumbers, initialAmount, percent, true);
            DebitAccounts.Add(newDebitAccount);
            client.DebitAccounts.Add(newDebitAccount);
            _accountNumbers++;
            return newDebitAccount;
        }

        public CreditAccount CreateCreditAccount(Client client, int initialAmount, int limit, int commission)
        {
            var newCreditAccount = new CreditAccount(_accountNumbers, initialAmount, limit, commission, true);
            CreditAccounts.Add(newCreditAccount);
            client.CreditAccounts.Add(newCreditAccount);
            _accountNumbers++;
            return newCreditAccount;
        }

        public DepositAccount CreateDepositAccount(Client client, int initialAmount, int duration)
        {
            var newDepositAccount = new DepositAccount(_accountNumbers, initialAmount, false, duration, Table);
            DepositAccounts.Add(newDepositAccount);
            client.DepositAccounts.Add(newDepositAccount);
            _accountNumbers++;
            return newDepositAccount;
        }

        public void Withdrawal(int withdrawalAmount, string accountName)
        {
            foreach (DebitAccount account in DebitAccounts.Where(account => account.Name == accountName))
            {
                account.Withdrawal(withdrawalAmount);
                var newTransaction =
                    new Transaction("W", Transactions.Count, withdrawalAmount, accountName, accountName);
                Transactions.Add(newTransaction);
                break;
            }

            foreach (CreditAccount account in CreditAccounts.Where(account => account.Name == accountName))
            {
                account.Withdrawal(withdrawalAmount);
                var newTransaction =
                    new Transaction("W", Transactions.Count, withdrawalAmount, accountName, accountName);
                Transactions.Add(newTransaction);
                break;
            }

            foreach (DepositAccount account in DepositAccounts.Where(account => account.Name == accountName))
            {
                account.Withdrawal(withdrawalAmount);
                var newTransaction =
                    new Transaction("W", Transactions.Count, withdrawalAmount, accountName, accountName);
                Transactions.Add(newTransaction);
                break;
            }
        }

        public void Replenishment(int replenishmentAmount, string accountName)
        {
            foreach (DebitAccount account in DebitAccounts.Where(account => account.Name == accountName))
            {
                account.Replenishment(replenishmentAmount);
                var newTransaction =
                    new Transaction("R", Transactions.Count, replenishmentAmount, accountName, accountName);
                Transactions.Add(newTransaction);
                break;
            }

            foreach (CreditAccount account in CreditAccounts.Where(account => account.Name == accountName))
            {
                account.Replenishment(replenishmentAmount);
                var newTransaction =
                    new Transaction("R", Transactions.Count, replenishmentAmount, accountName, accountName);
                Transactions.Add(newTransaction);
                break;
            }

            foreach (DepositAccount account in DepositAccounts.Where(account => account.Name == accountName))
            {
                account.Replenishment(replenishmentAmount);
                var newTransaction =
                    new Transaction("R", Transactions.Count, replenishmentAmount, accountName, accountName);
                Transactions.Add(newTransaction);
                break;
            }
        }

        public void СancellationOfTheTransaction(int number)
        {
            foreach (Transaction transaction in Transactions.Where(transaction => transaction.Number == number))
            {
                switch (transaction.TransactionType)
                {
                    case "W":
                        Replenishment(transaction.Amount, transaction.Sender);
                        break;
                    case "R":
                        Withdrawal(transaction.Amount, transaction.Recipient);
                        break;
                }
            }
        }

        public int AccountAfterTime(DebitAccount account, DateTime date)
        {
            var additionalAmounts = new List<int>();
            int tempAmount = account.Amount;
            int numberOfElapsedMonth =
                ((date.Year - account.OpeningDate.Year) * 12) + date.Month - account.OpeningDate.Month;

            // int numberOfElapsedDays = (date - account.OpeningDate).Duration().Days;
            for (int elapsedMonth = 0; elapsedMonth < numberOfElapsedMonth; elapsedMonth++)
            {
                for (int elapsedDay = 0; elapsedDay <= 30; elapsedDay++)
                {
                    additionalAmounts[elapsedDay] = tempAmount * (account.Percent / 100 / 365);
                    tempAmount += additionalAmounts[elapsedDay];
                }

                account.Amount = tempAmount;
                tempAmount = account.Amount;
                additionalAmounts.Clear();
            }

            return account.Amount;
        }

        public int AccountAfterTime(DepositAccount account, DateTime date)
        {
            var additionalAmounts = new List<int>();
            int tempAmount = account.Amount;
            int numberOfElapsedMonth =
                ((date.Year - account.OpeningDate.Year) * 12) + date.Month - account.OpeningDate.Month;

            // int numberOfElapsedDays = (date - account.OpeningDate).Duration().Days;
            for (int elapsedMonth = 0; elapsedMonth < numberOfElapsedMonth; elapsedMonth++)
            {
                for (int elapsedDay = 0; elapsedDay <= 30; elapsedDay++)
                {
                    additionalAmounts[elapsedDay] = tempAmount * (account.Percent / 100 / 365);
                    tempAmount += additionalAmounts[elapsedDay];
                }

                account.Amount = tempAmount;
                tempAmount = account.Amount;
                additionalAmounts.Clear();
            }

            return account.Amount;
        }

        public int AccountAfterTime(CreditAccount account, DateTime date)
        {
            int numberOfElapsedMonth =
                ((date.Year - account.OpeningDate.Year) * 12) + date.Month - account.OpeningDate.Month;
            for (int elapsedMonth = 0; elapsedMonth < numberOfElapsedMonth; elapsedMonth++)
            {
                if (account.Amount < 0)
                {
                    account.Amount -= account.Commission;
                }
            }

            return account.Amount;
        }
    }
}