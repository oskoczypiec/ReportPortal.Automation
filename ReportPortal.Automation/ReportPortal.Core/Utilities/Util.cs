// <copyright file="Util.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Core.Utilities
{
    public static class Util
    {
        public static string GetCurrentDateTime()
        {
            return DateTime.Now.ToString("T’HH’:’mm’:’ss.fffffffK");
        }
    }
}
