namespace ReportPortal.Core.Data
{
    public class GenericEndpoints
    {
        public static string GenerateDemoData(string project = "default_personal") => $"/api/v1/demo/{project}/generate";
    }
}
