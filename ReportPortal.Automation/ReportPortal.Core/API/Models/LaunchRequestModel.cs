namespace ReportPortal.Core.API.Models
{
    using Newtonsoft.Json;

    public class LaunchRequestModel
    {
        [JsonProperty("ids")]
        public List<int> LaunchIds { get; set; }
    }
}
