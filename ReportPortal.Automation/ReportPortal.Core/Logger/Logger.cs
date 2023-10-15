// <copyright file="Logger.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Core.Logger
{
    /// <summary>
    /// Provides logging capabilities for the test framework.
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// Gets static instance of ConsoleLogger.
        /// </summary>
        public static ConsoleLogger Log { get; } = new ();
    }
}
