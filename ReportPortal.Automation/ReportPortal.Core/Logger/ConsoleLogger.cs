// <copyright file="ConsoleLogger.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Core.Logger
{
    using Serilog;

    public class ConsoleLogger
    {
        private const string LogFilePath = "logs/logs.txt";
        private ILogger _logger;

        public ConsoleLogger()
        {
            if(File.Exists(LogFilePath))
            {
                File.Delete(LogFilePath);
            }

            _logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File(LogFilePath)
                .CreateLogger();
        }

        public void CloseLogger()
        {
            Log.CloseAndFlush();
        }

        public void Info(string message) => _logger.Information(message);

        public void Debug(string message) => _logger.Debug(message);

        public void Warn(string message) => _logger.Warning(message);

        public void Error(string message) => _logger.Error(message);
    }
}
