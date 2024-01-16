// <copyright file="LaunchEndpoint.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Core.Data
{
    public static class LaunchEndpoint
    {
        public static string GetLaunchesByProjectName(string projectName = "default_personal") => $"/api/v1/{projectName}/launch";
    }
}
