using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Banks
{
    public class MainBank
    {
        public MainBank(string name, string country)
        {
            Name = name;
            Country = country;
            Banks = new List<Bank>();
            Transactions = new List<Transaction>();
        }

        public string Name { get; set; }
        public string Country { get; set; }
        public List<Bank> Banks { get; set; }
        public List<Transaction> Transactions { get; set; }

        public Bank CreateBank(string name, List<AmountPercentPair> table)
        {
            var bank = new Bank(name, table);
            Banks.Add(bank);
            return bank;
        }

        public void Transfer(int transferAmount, string senderName, string recipientName, Bank bank)
        {
            if (transferAmount < 0)
            {
                throw new Exception();
            }

            var newTransaction =
                new Transaction("T", Transactions.Count, transferAmount, senderName, recipientName, bank);
            bank.Withdrawal(transferAmount, senderName);
            bank.Replenishment(transferAmount, recipientName);

            Transactions.Add(newTransaction);
        }

        public void Transfer(int transferAmount, string senderName, string recipientName, Bank bank1, Bank bank2)
        {
            if (transferAmount < 0)
            {
                throw new Exception();
            }

            var newTransaction =
                new Transaction("T", Transactions.Count, transferAmount, senderName, recipientName, bank1, bank2);
            bank1.Withdrawal(transferAmount, senderName);
            bank2.Replenishment(transferAmount, recipientName);
            Transactions.Add(newTransaction);
        }

        public void СancellationOfTheTransaction(int number)
        {
            foreach (Transaction transaction in Transactions.Where(transaction => transaction.Number == number))
            {
                transaction.SenderBank.Replenishment(transaction.Amount, transaction.Sender);
                transaction.RecipientBank.Withdrawal(transaction.Amount, transaction.Recipient);
            }
        }
    }
}