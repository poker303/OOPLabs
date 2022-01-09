namespace Banks.Exceptions
{
    public class AmountException : BanksException
    {
        public AmountException(string message)
            : base(message)
        {
        }
    }
}