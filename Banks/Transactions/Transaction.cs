using System;
using Banks.Accounts;

namespace Banks
{
    public class Transaction
    {
        private bool _wasCanceled;

        public Transaction(int number, int amount, IAccount sender, IAccount recipient)
        {
            Number = number;
            Amount = amount;
            WithdrawnAccount = sender;
            RecipientAccount = recipient;
            _wasCanceled = false;
        }

        public int Number { get; }
        public int Amount { get; }
        public IAccount WithdrawnAccount { get; }
        public IAccount RecipientAccount { get; }

        public virtual void СancellationOfTheTransaction() { }
        public void ChangeCancellation()
        {
            _wasCanceled = true;
        }

        public bool GetCancellation()
        {
            return _wasCanceled;
        }
    }
}