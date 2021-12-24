namespace Backups.Exceptions
{
    public class AddingDirectoryException : BackupsException
    {
        public AddingDirectoryException(string message)
            : base(message)
        {
        }
    }
}