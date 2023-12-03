namespace ReportPortal.Core.Data
{
    public static class DashboardEndpoint
    {
        public static string DeleteDashboardByProjectNameAndId(string id, string projectName) => $"/api/v1/{projectName}/dashboard/{id}";
    }
}
