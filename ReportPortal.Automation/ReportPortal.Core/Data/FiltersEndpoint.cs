namespace ReportPortal.Core.Data
{
    public static class FiltersEndpoint
    {
        public static string FilterByProject(string projectName = "default_personal") => $"/api/v1/{projectName}/filter";

        public static string FilterById(string id, string projectName) => $"/api/v1/{projectName}/filter/{id}";

        public static string DeleteFilterByProjectNameAndId(int id, string projectName) => $"/api/v1/{projectName}/filter/{id}";

        public static string FilterNames(string project) => $"/api/v1/{project}/filter/names";
    }
}
