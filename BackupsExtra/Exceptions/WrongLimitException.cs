namespace BackupsExtra.Exceptions
{
    public class WrongLimitException : BackupsExtraException
    {
        public WrongLimitException(string message)
            : base(message)
        {
        }
    }
}