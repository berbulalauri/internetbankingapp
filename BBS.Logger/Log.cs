using NLog;
using System;
using ILogger = BBS.Interfaces.ILogger;

namespace BBS.Logger
{
    public class Log : ILogger
    {
        private readonly NLog.Logger _logger = LogManager.GetCurrentClassLogger();

        public void Error(Exception exception, string message)
        {
            _logger.Error(exception, message);
        }

        public void Error(Exception exception)
        {
            _logger.Error(exception);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }
    }
}
