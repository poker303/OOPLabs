namespace Banks.Exceptions
{
    public class SubscriptionException : BanksException
    {
        public SubscriptionException(string message)
            : base(message)
        {
        }
    }
}