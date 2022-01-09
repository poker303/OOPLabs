namespace Banks.Exceptions
{
    public class TimeException : BanksException
    {
        public TimeException(string message)
            : base(message)
        {
        }
    }
}