namespace ReportPortal.Core.API.Models
{
    using Newtonsoft.Json;

    public class DemoDataGeneratedModel
    {
        [JsonProperty("dashboardId")]
        public int DashboardId { get; set; }

        [JsonProperty("launchIds")]
        public List<int> LaunchIds { get; set; }
    }
}
