using System;
using Banks.Accounts;

namespace Banks
{
    public class Transaction
    {
        public Transaction(string type, int number, int amount, IAccount account, DateTime creationDate)
        {
            TransactionType = type;
            Number = number;
            Amount = amount;
            Account = account;
            CreationDate = creationDate;
        }

        public Transaction(string type, int number, int amount, IAccount account1, IAccount account2, Bank bank, DateTime creationDate)
        {
            TransactionType = type;
            Number = number;
            Amount = amount;
            Sender = account1;
            Recipient = account2;
            TransactionBank = bank;
            CreationDate = creationDate;
        }

        public Transaction(string type, int number, int amount, IAccount account1, IAccount account2, Bank bank1, Bank bank2, DateTime creationDate)
        {
            TransactionType = type;
            Number = number;
            Amount = amount;
            Sender = account1;
            Recipient = account2;
            SenderBank = bank1;
            RecipientBank = bank2;
            CreationDate = creationDate;
        }

        public Bank RecipientBank { get; }

        public Bank SenderBank { get; }

        public string TransactionType { get; }
        public int Number { get; }
        public int Amount { get; }
        public IAccount Sender { get; }
        public IAccount Recipient { get; }

        private DateTime CreationDate { get; }
        private Bank TransactionBank { get; }

        private IAccount Account { get; }
    }
}