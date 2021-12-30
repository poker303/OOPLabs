using Serilog;
using Serilog.Core;

namespace BackupsExtra
{
    public class FileLogger : ILoggerService
    {
        public void LoggerOutput(bool logTimeCodeOn, string outPutMessage)
        {
            if (logTimeCodeOn)
            {
                const string path = "C:/Users/Алексей/OneDrive/Документы/GitHub/poker303/BackupsExtra/Loggs.txt";
                using Logger logger = new LoggerConfiguration().WriteTo.File(path, outputTemplate: "{Timestamp:dd.MM.yy HH:mm:ss} {Message}{NewLine}{Exception}").CreateLogger();
                logger.Information(outPutMessage);
            }
            else
            {
                const string path = "C:/Users/Алексей/OneDrive/Документы/GitHub/poker303/BackupsExtra/Loggs.txt";
                using Logger logger = new LoggerConfiguration().WriteTo.File(path, outputTemplate: "{Message}{NewLine}")
                    .CreateLogger();
                logger.Information(outPutMessage);
            }
        }
    }
}