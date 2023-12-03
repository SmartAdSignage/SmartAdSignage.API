//using Microsoft.Extensions.Logging;
using SmartAdSignage.Services.Services.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Services.Services.Implementations
{
    public class LoggerService : ILoggerService
    {
        private readonly ILogger logger;
        public LoggerService(ILogger logger) 
        {
            this.logger = logger;
        }
        public void LogDebug(string message)
        {
            logger.Debug(message);
        }
        public void LogError(string message)
        {
            logger.Error(message);
        }
        public void LogInformation(string message)
        {
            logger.Information(message);
        }
        public void LogWarning(string message)
        {
            logger.Warning(message);
            /*logger.Fa*/
        }
    }
}
