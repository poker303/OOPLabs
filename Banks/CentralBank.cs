using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Banks.Accounts;
using Banks.Exceptions;
using Banks.TransmittedParameters;

namespace Banks
{
    public class CentralBank
    {
        public CentralBank(string name, string country)
        {
            Name = name;
            Country = country;
            Banks = new List<Bank>();
            Transactions = new List<Transaction>();
        }

        public string Name { get; }
        public List<Bank> Banks { get; }
        private string Country { get; }
        private List<Transaction> Transactions { get; }

        public Bank CreateBank(BankParameters parameters)
        {
            var bank = new Bank(parameters);
            Banks.Add(bank);
            return bank;
        }

        public void Transfer(int transferAmount, IAccount sender, IAccount recipient, Bank bank, DateTime operationDate)
        {
            if (transferAmount < 0)
            {
                throw new AmountException("The amount specified is incorrect, please change it.");
            }

            var newTransaction =
                new Transaction(Transactions.Count, transferAmount, sender, recipient);
            bank.Withdrawal(transferAmount, newTransaction.WithdrawnAccount, operationDate);
            bank.Replenishment(transferAmount, newTransaction.RecipientAccount, operationDate);

            Transactions.Add(newTransaction);
        }

        public void Transfer(int transferAmount, IAccount sender, IAccount recipient, Bank bank1, Bank bank2, DateTime operationDate)
        {
            if (transferAmount < 0)
            {
                throw new AmountException("The amount specified is incorrect, please change it.");
            }

            var newTransaction = new TransferBetweenAccounts(Transactions.Count, transferAmount, sender, recipient);
            bank1.Withdrawal(transferAmount, newTransaction.WithdrawnAccount, operationDate);
            bank2.Replenishment(transferAmount, newTransaction.RecipientAccount, operationDate);
            Transactions.Add(newTransaction);
        }

        public void СancellationOfTheTransaction(int number)
        {
            foreach (Transaction transaction in Transactions.Where(transaction => transaction.Number == number))
            {
                transaction.СancellationOfTheTransaction();
            }
        }

        public void Notification(DateTime operationDate)
        {
            foreach (Bank bank in Banks)
            {
                foreach (IAccount account in bank.CreditAccounts.Concat(bank.DebitAccounts).Concat(bank.DepositAccounts))
                {
                    bank.Accruals(account, operationDate);
                }
            }
        }
    }
}