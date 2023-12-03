// <copyright file="FiltersRootModel.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Core.API.Models
{
    using Newtonsoft.Json;

    public class Condition
    {
        [JsonProperty("filteringField")]
        public string FilteringField { get; set; }

        [JsonProperty("condition")]
        public string ConditionText { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class Content
    {
        [JsonProperty("owner")]
        public string Owner { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("conditions")]
        public List<Condition> Conditions { get; set; }

        [JsonProperty("orders")]
        public List<Order> Orders { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class Order
    {
        [JsonProperty("sortingColumn")]
        public string SortingColumn { get; set; }

        [JsonProperty("isAsc")]
        public bool IsAsc { get; set; }
    }

    public class Page
    {
        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("totalElements")]
        public int TotalElements { get; set; }

        [JsonProperty("totalPages")]
        public int TotalPages { get; set; }
    }

    public class FiltersRootModel
    {
        [JsonProperty("content")]
        public List<Content> Content { get; set; }

        [JsonProperty("page")]
        public Page Page { get; set; }
    }

    public class Element
    {
        [JsonProperty("conditions")]
        public List<Condition> Conditions { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("orders")]
        public List<Order> Orders { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class ElementsRoot
    {
        [JsonProperty("elements")]
        public List<Element> Elements { get; set; }
    }

    public class FiltersId
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
