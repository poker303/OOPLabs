using Serilog;
using Serilog.Core;

namespace BackupsExtra
{
    public class ConsoleLogger : ILoggerService
    {
        public void LoggerOutput(bool logTimeCodeOn, string outPutMessage)
        {
            if (logTimeCodeOn)
            {
                using Logger logger = new LoggerConfiguration().WriteTo.Console(outputTemplate: "{Timestamp:dd.MM.yy HH:mm:ss} {Message}{NewLine}{Exception}").CreateLogger();
                logger.Information(outPutMessage);
            }
            else
            {
                using Logger logger = new LoggerConfiguration().WriteTo.Console(outputTemplate: "{Message}{NewLine}")
                    .CreateLogger();
                logger.Information(outPutMessage);
            }
        }
    }
}