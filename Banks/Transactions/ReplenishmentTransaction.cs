using Banks.Accounts;
using Banks.Exceptions;

namespace Banks
{
    public class ReplenishmentTransaction : Transaction
    {
        public ReplenishmentTransaction(int number, int amount, IAccount sender, IAccount recipient)
            : base(number, amount, sender, recipient)
        {
        }

        public override void СancellationOfTheTransaction()
        {
            if (GetCancellation())
            {
                throw new OperationException("This operation's already been canceled.");
            }

            RecipientAccount.Withdrawal(Amount);
            ChangeCancellation();
        }
    }
}