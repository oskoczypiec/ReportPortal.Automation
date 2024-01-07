// <copyright file="NewFilterModel.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Core.API.Models
{
    using Newtonsoft.Json;

    public class NewFilterModel
    {
        [JsonProperty("conditions")]
        public List<Condition> Conditions { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("orders")]
        public List<Order> Orders { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
