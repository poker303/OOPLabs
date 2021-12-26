using Banks.Accounts;
using Banks.Exceptions;

namespace Banks
{
    public class TransferBetweenAccounts : Transaction
    {
        public TransferBetweenAccounts(int number, int amount, IAccount sender, IAccount recipient)
            : base(number, amount, sender, recipient)
        {
        }

        public override void СancellationOfTheTransaction()
        {
            if (GetCancellation())
            {
                throw new OperationException("This operation's already been canceled.");
            }

            WithdrawnAccount.Replenishment(Amount);
            RecipientAccount.Withdrawal(Amount);
            ChangeCancellation();
        }
    }
}