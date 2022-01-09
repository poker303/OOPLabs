using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Accounts;
using Banks.Exceptions;
using Banks.TransmittedParameters;

namespace Banks
{
    public class Bank
    {
        private readonly List<Client> _observers;
        private int _accountNumbers = 1;

        public Bank(BankParameters parameters)
        {
            _observers = new List<Client>();
            Name = parameters.Name;
            ReliabilityAmount = parameters.ReliabilityAmount;

            Table = parameters.Table;
            DepositCommission = parameters.DepositCommission;

            DebitPercent = parameters.DebitPercent;
            DebitCommission = parameters.DebitCommission;

            CreditPercent = parameters.CreditPercent;
            CreditCommission = parameters.CreditCommission;
            CreditLimit = parameters.CreditLimit;

            Clients = new List<Client>();
            CreditAccounts = new List<IAccount>();
            DepositAccounts = new List<IAccount>();
            DebitAccounts = new List<IAccount>();
            Transactions = new List<Transaction>();
        }

        public List<Client> Clients { get; }
        public List<IAccount> CreditAccounts { get; }
        public List<IAccount> DepositAccounts { get; }
        public List<IAccount> DebitAccounts { get; }

        private List<Transaction> Transactions { get; }
        private string Name { get; }

        private AmountPercentPair Table { get; }

        private int ReliabilityAmount { get; set; }

        private int DebitPercent { get; }

        private int DebitCommission { get; }

        private int CreditPercent { get; }

        private int CreditCommission { get; }

        private int DepositCommission { get; }

        private int CreditLimit { get; }

        public string GetName()
        {
            return Name;
        }

        public void AddClients(List<Client> clients)
        {
            foreach (Client client in clients.Where(client => !Clients.Contains(client)))
            {
                Clients.Add(client);
            }
        }

        public DebitAccount CreateDebitAccount(Client client, int initialAmount, DateTime creationTime, int duration)
        {
            bool reliability = CheckingAccount(client, initialAmount);

            var parameters = new AccountParameters(_accountNumbers, initialAmount, DebitCommission, true, creationTime, duration, reliability, ReliabilityAmount);
            var newDebitAccount = new DebitAccount(parameters, DebitPercent);

            DebitAccounts.Add(newDebitAccount);
            client.DebitAccounts.Add(newDebitAccount);
            _accountNumbers++;
            return newDebitAccount;
        }

        public CreditAccount CreateCreditAccount(Client client, int initialAmount, DateTime creationTime, int duration)
        {
            bool reliability = CheckingAccount(client, initialAmount);

            var parameters = new AccountParameters(_accountNumbers, initialAmount, CreditCommission, true, creationTime, duration, reliability, ReliabilityAmount);
            var newCreditAccount = new CreditAccount(parameters, CreditPercent, CreditLimit);
            CreditAccounts.Add(newCreditAccount);
            client.CreditAccounts.Add(newCreditAccount);
            _accountNumbers++;
            return newCreditAccount;
        }

        public DepositAccount CreateDepositAccount(Client client, int initialAmount, DateTime creationTime, int duration)
        {
            bool reliability = CheckingAccount(client, initialAmount);

            var parameters = new AccountParameters(_accountNumbers, initialAmount, DebitCommission, false, creationTime, duration, reliability, ReliabilityAmount);
            var newDepositAccount = new DepositAccount(parameters, Table);
            DepositAccounts.Add(newDepositAccount);
            client.DepositAccounts.Add(newDepositAccount);
            _accountNumbers++;
            return newDepositAccount;
        }

        public void Withdrawal(int withdrawalAmount, IAccount account, DateTime operationDate)
        {
            if (account.Reliability == false)
            {
                if (withdrawalAmount > account.ReliabilityAmount)
                {
                    throw new OperationException("To perform the operation, enter your passport number.");
                }
            }

            if (account is DepositAccount)
            {
                if (account.CloseDate > operationDate)
                {
                    throw new TimeException("The operation is impossible.");
                }
            }

            account.Withdrawal(withdrawalAmount);
            var newTransaction = new WithdrawalTransaction(Transactions.Count, withdrawalAmount, account, account);
            Transactions.Add(newTransaction);
        }

        public void Replenishment(int replenishmentAmount, IAccount account, DateTime operationDate)
        {
                account.Replenishment(replenishmentAmount);
                var newTransaction = new ReplenishmentTransaction(Transactions.Count, replenishmentAmount, account, account);
                Transactions.Add(newTransaction);
        }

        public void СancellationOfTheTransaction(int number)
        {
            foreach (Transaction transaction in Transactions.Where(transaction => transaction.Number == number))
            {
                transaction.СancellationOfTheTransaction();
            }
        }

        public void Accruals(IAccount account, DateTime date)
        {
            int numberOfElapsedDays = (date - account.OpeningDate).Duration().Days;

            double tempPercent = account.Percent / 36500d;

            for (int day = 0; day < numberOfElapsedDays; day++)
            {
                account.Amount += account.Amount * tempPercent;
                if (account.Amount < 0)
                {
                    account.Amount -= account.Commission;
                }
            }
        }

        public void ClientAccountsUpdate(Client client, DateTime operationDate)
        {
            if (client.PassportNumber != null && client.Address != null)
            {
                foreach (IAccount account in client.CreditAccounts.Concat(client.DebitAccounts).Concat(client.DepositAccounts))
                {
                    account.Reliability = true;
                }
            }

            foreach (IAccount account in client.DepositAccounts)
            {
                if (operationDate > account.CloseDate)
                {
                    account.PossibilityOfWithdrawal = true;
                }
            }
        }

        public void AttachObserver(Client client)
        {
            if (client.GetSubscriptionDesire() == false) throw new SubscriptionException("No subscription permission.");
            if (!_observers.Contains(client))
            {
                _observers.Add(client);
            }
        }

        public void DetachObserver(Client client)
        {
            _observers.Remove(client);
        }

        public void NotifyObservers(IAccount account)
        {
            foreach (Client observer in _observers)
            {
                if (observer.CreditAccounts.Concat(observer.DebitAccounts).Concat(observer.DepositAccounts).Contains(account))
                {
                    observer.Update(account);
                }
            }
        }

        public void ChangeDebitAccountOptions(IAccount account, int newDebitPercent, int newDebitCommission)
        {
            account.Commission = newDebitCommission;
            account.Percent = newDebitPercent;
            NotifyObservers(account);
        }

        public void ChangeDepositAccountOptions(DepositAccount account, int newDepositCommission)
        {
            account.Commission = newDepositCommission;
            NotifyObservers(account);
        }

        public void ChangeCreditAccountOptions(CreditAccount account, int newCreditPercent, int newCreditCommission, int newCreditLimit)
        {
            account.Commission = newCreditCommission;
            account.Percent = newCreditPercent;
            account.Limit = newCreditLimit;
            NotifyObservers(account);
        }

        public void ChangeReliabilityAmount(int newReliabilityAmount)
        {
            ReliabilityAmount = newReliabilityAmount;
        }

        public bool CheckingAccount(Client client, int initialAmount)
        {
            if (initialAmount > client.Money)
            {
                throw new AmountException("You don't have that much money");
            }

            client.Money -= initialAmount;

            return client.PassportNumber != null && client.Address != null;
        }
    }
}