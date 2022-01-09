using System;
using Banks.Accounts;
using Banks.Exceptions;

namespace Banks
{
    public class WithdrawalTransaction : Transaction
    {
        public WithdrawalTransaction(int number, int amount, IAccount sender, IAccount account2)
            : base(number, amount, sender, account2)
        {
        }

        public override void СancellationOfTheTransaction()
        {
            if (GetCancellation())
            {
                throw new OperationException("This operation's already been canceled.");
            }

            WithdrawnAccount.Replenishment(Amount);
            ChangeCancellation();
        }
    }
}