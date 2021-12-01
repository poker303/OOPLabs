using System;

namespace Banks
{
    public class Transaction
    {
        public Transaction(string type, int number, int amount, string accountName1, string accountName2)
        {
            TransactionType = type;
            Number = number;
            Amount = amount;
            Sender = accountName1;
            Recipient = accountName2;
        }

        public Transaction(string type, int number, int amount, string accountName1, string accountName2, Bank bank)
        {
            TransactionType = type;
            Number = number;
            Amount = amount;
            Sender = accountName1;
            Recipient = accountName2;
            TransactionBank = bank;
        }

        public Transaction(string type, int number, int amount, string accountName1, string accountName2, Bank bank1, Bank bank2)
        {
            TransactionType = type;
            Number = number;
            Amount = amount;
            Sender = accountName1;
            Recipient = accountName2;
            SenderBank = bank1;
            RecipientBank = bank2;
        }

        public Bank TransactionBank { get; set; }
        public Bank RecipientBank { get; set; }

        public Bank SenderBank { get; set; }

        public string TransactionType { get; set; }
        public int Number { get; set; }
        public int Amount { get; set; }
        public string Recipient { get; set; }

        public string Sender { get; set; }
    }
}