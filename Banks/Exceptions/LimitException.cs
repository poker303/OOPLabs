namespace Banks.Exceptions
{
    public class LimitException : BanksException
    {
        public LimitException(string message)
            : base(message)
        {
        }
    }
}