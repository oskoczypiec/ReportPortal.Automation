// <copyright file="ConsoleLogger.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Core.Logger
{
    using Serilog;

    /// <summary>
    /// Provides logging capabilities with the ability to log messages at various levels (Info, Debug, Warn, Error) to the console and a log file.
    /// </summary>
    public class ConsoleLogger
    {
        private const string LogFilePath = "logs/logs.txt";
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleLogger"/> class. It configures the logger to log messages to the console and a log file.
        /// </summary>
        public ConsoleLogger()
        {
            if (File.Exists(LogFilePath))
            {
                File.Delete(LogFilePath);
            }

            this.logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File(LogFilePath)
                .CreateLogger();
        }

        /// <summary>
        /// Closes and flushes the logger, ensuring all log messages are written before finishing the logging.
        /// </summary>
        public void CloseLogger()
        {
            Log.CloseAndFlush();
        }

        /// <summary>
        /// Logs an information message.
        /// </summary>
        /// <param name="message">The information message to log.</param>
        public void Info(string message) => this.logger.Information(message);

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="message">The debug message to log.</param>
        public void Debug(string message) => this.logger.Debug(message);

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="message">The warning message to log.</param>
        public void Warn(string message) => this.logger.Warning(message);

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">The error message to log.</param>
        public void Error(string message) => this.logger.Error(message);
    }
}
