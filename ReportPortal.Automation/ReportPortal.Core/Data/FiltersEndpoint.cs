namespace ReportPortal.Core.Data
{
    public static class FiltersEndpoint
    {
        public static string Filter => "/api/v1/default_personal/filter";

        public static string Filters => "/api/v1/default_personal/filter/filters";

        public static string Names => "/api/v1/default_personal/filter/names";

        public static string FilterById(string id) => $"/api/v1/default_personal/filter/{id}";
    }
}
