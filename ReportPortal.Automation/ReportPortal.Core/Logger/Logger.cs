// <copyright file="Logger.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Core.Logger
{
    public static class Logger
    {
        public static ConsoleLogger Log { get; } = new();
    }
}
