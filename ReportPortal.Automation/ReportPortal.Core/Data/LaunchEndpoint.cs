namespace ReportPortal.Core.Data
{
    public static class LaunchEndpoint
    {
        public static string GetAllLaunchesByProjectName(string projectName = "default_personal") => $"/api/v1/{projectName}/launch";
    }
}
