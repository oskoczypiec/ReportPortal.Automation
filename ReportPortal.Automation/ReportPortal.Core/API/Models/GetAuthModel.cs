using Newtonsoft.Json;

namespace ReportPortal.Core.API.Models
{
    public class GetAuthModel
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
