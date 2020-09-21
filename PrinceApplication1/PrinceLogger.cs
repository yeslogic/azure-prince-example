using Microsoft.Extensions.Logging;

namespace PrinceApplication1
{
    public class PrinceLogger : PrinceEvents
    {
        private readonly ILogger _logger;

        public PrinceLogger(ILogger logger)
        {
            _logger = logger;
        }

        /**
         * This method will be called when a warning or error message is received
         * from Prince.
         * @param msgType The type of the message ("inf", "wrn", or "err").
         * @param msgLocation The name of the file that the message refers to.
         * This may be empty if the message does not refer to any particular file.
         * @param msgText The text of the message.
         */
        public void onMessage(string msgType, string msgLocation, string msgText)
        {
            LogLevel logLevel;
            switch (msgType)
            {
                case "dbg":
                    logLevel = LogLevel.Debug;
                    break;
                case "inf":
                    logLevel = LogLevel.Information;
                    break;
                case "wrn":
                    logLevel = LogLevel.Warning;
                    break;
                case "err":
                    logLevel = LogLevel.Error;
                    break;
                default:
                    logLevel = LogLevel.Trace;
                    break;
            }

            if (msgLocation == "")
            {
                _logger.Log(logLevel, "{msgText}", msgText);
            }
            else
            {
                _logger.Log(logLevel, "{msgLocation}: {msgText}", msgLocation, msgText);
            }
        }
    }
}
