namespace Banks.Exceptions
{
    public class OperationException : BanksException
    {
        public OperationException(string message)
            : base(message)
        {
        }
    }
}