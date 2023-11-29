using Microsoft.Extensions.Logging;
using SmartAdSignage.Services.Services.Interfaces;
//using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Services.Services.Implementations
{
    public class LoggerService : ILoggerService
    {
        private readonly ILogger<LoggerService> logger;
        public LoggerService(ILogger<LoggerService> logger) 
        {
            this.logger = logger;
        }
        public void LogDebug(string message)
        {
            logger.LogDebug(message);
        }
        public void LogError(string message)
        {
            logger.LogError(message);
        }
        public void LogInformation(string message)
        {
            logger.LogInformation(message);
        }
        public void LogWarning(string message)
        {
            logger.LogWarning(message);
        }
    }
}
