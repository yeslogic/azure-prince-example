using Microsoft.Extensions.Logging;
using PrinceXML.Wrapper.Events;

namespace PrinceApplication1
{
    public class PrinceLogger : PrinceEvents
    {
        private readonly ILogger _logger;

        public PrinceLogger(ILogger logger)
        {
            _logger = logger;
        }

        public void OnMessage(MessageType msgType, string msgLocation, string msgText)
        {
            LogLevel logLevel;
            switch (msgType)
            {
                case MessageType.DBG:
                    logLevel = LogLevel.Debug;
                    break;
                case MessageType.INF:
                    logLevel = LogLevel.Information;
                    break;
                case MessageType.WRN:
                    logLevel = LogLevel.Warning;
                    break;
                case MessageType.ERR:
                    logLevel = LogLevel.Error;
                    break;
                case MessageType.OUT:
                    logLevel = LogLevel.Trace;
                    break;
                default:
                    logLevel = LogLevel.None;
                    break;
            }

            if (msgLocation == "")
            {
                _logger.Log(logLevel, "[message] {msgText}", msgText);
            }
            else
            {
                _logger.Log(logLevel, "[message] {msgLocation}: {msgText}", msgLocation, msgText);
            }
        }

        public void OnDataMessage(string name, string value)
        {
            _logger.Log(LogLevel.Information, "[data] {name}: {value}", name, value);
        }
    }
}
