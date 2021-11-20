namespace Backups.Exceptions
{
    public class InvalidStorageTypeException : BackupsException
    {
        public InvalidStorageTypeException(string message)
            : base(message)
        {
        }
    }
}