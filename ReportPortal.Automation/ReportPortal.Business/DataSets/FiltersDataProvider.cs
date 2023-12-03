// <copyright file="FiltersDataProvider.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Business.DataSets
{
    using ReportPortal.Core.API.Models;
    using ReportPortal.Core.Utilities;

    public class FiltersDataProvider
    {
        public FiltersRootModel GenerateRealFilterResponse(FiltersRootModel response)
        {
            return new FiltersRootModel()
            {
                Content = this.GenerateContent(response),
                Page = new Page()
                {
                    Number = response.Page.Number,
                    Size = response.Page.Size,
                    TotalElements = response.Page.TotalElements,
                    TotalPages = response.Page.TotalPages,
                },
            };
        }

        private List<Content> GenerateContent(FiltersRootModel response)
        {
            var contentList = new List<Content>();
            for (int i = 0; i < response.Content.Count(); i++)
            {
                contentList.Add(new Content()
                {
                    Conditions = response.Content[i].Conditions,
                    Id = response.Content[i].Id,
                    Name = response.Content[i].Name,
                    Orders = response.Content[i].Orders,
                    Owner = response.Content[i].Owner,
                    Type = response.Content[i].Type,
                });
            }

            return contentList;
        }

        public FiltersRootModel GenerateRandomGetFiltersResponse()
        {
            return new FiltersRootModel()
            {
                Content = new List<Content>()
                {
                    new Content()
                    {
                        Owner = RandomHelper.AlphabeticalString(10),
                        Id = RandomHelper.RandomNumber(),
                        Name = RandomHelper.AlphabeticalString(10),
                        Orders = new List<Order>()
                        {
                            new Order()
                            {
                                IsAsc = false,
                                SortingColumn = RandomHelper.AlphabeticalString(10),
                            },
                        },
                        Conditions = new List<Condition>()
                        {
                            new Condition()
                            {
                                ConditionText = RandomHelper.AlphabeticalString(10),
                                FilteringField = RandomHelper.AlphabeticalString(10),
                                Value = RandomHelper.AlphabeticalString(10),
                            },
                        },
                        Type = RandomHelper.AlphabeticalString(10),
                    },
                },
                Page = new Page()
                {
                    Number = RandomHelper.RandomNumber(),
                    Size = RandomHelper.RandomNumber(),
                    TotalElements = RandomHelper.RandomNumber(),
                    TotalPages = RandomHelper.RandomNumber(),
                },
            };
        }

        public NewFilterModel GenerateRandomAddFiltersResponse()
        {
            return new NewFilterModel()
            {
                Conditions = new List<Condition>
                {
                    new Condition()
                    {
                        ConditionText = "gte",
                        FilteringField = "number",
                        Value = "3",
                    },
                },
                Description = RandomHelper.AlphabeticalString(10),
                Name = RandomHelper.AlphabeticalString(10),
                Orders = new List<Order>
                {
                    new Order()
                    {
                        IsAsc = true,
                        SortingColumn = "number",
                    },
                },
                Type = "launch",
            };
        }

        public NewFilterModel GenerateIncorrectAddFilters()
        {
            return new NewFilterModel()
            {
                Conditions = new List<Condition>
                {
                    new Condition()
                    {
                        ConditionText = RandomHelper.AlphabeticalString(5),
                        FilteringField = "number",
                        Value = "3",
                    },
                },
                Description = RandomHelper.AlphabeticalString(10),
                Name = RandomHelper.AlphabeticalString(10),
                Orders = new List<Order>
                {
                    new Order()
                    {
                        IsAsc = true,
                        SortingColumn = "number",
                    },
                },
                Type = "launch",
            };
        }

        public ElementsRoot GenerateRandomPutFiltersResponse(NewFilterModel? postFilterModel = null)
        {
            return new ElementsRoot()
            {
                Elements = new List<Element>()
                {
                    new Element()
                    {
                        Id = RandomHelper.AlphabeticalString(10),
                        Conditions = postFilterModel.Conditions == null ? new List<Condition>
                        {
                            new Condition()
                            {
                                ConditionText = "gte",
                                FilteringField = "number",
                                Value = "3",
                            },
                        } : postFilterModel.Conditions,
                        Description = postFilterModel.Description == null ? RandomHelper.AlphabeticalString(10) : postFilterModel.Description,
                        Name = postFilterModel.Name == null ? RandomHelper.AlphabeticalString(10) : postFilterModel.Name,
                        Orders = postFilterModel.Orders == null? new List<Order>
                        {
                            new Order()
                            {
                                IsAsc = true,
                                SortingColumn = "number",
                            },
                        } : postFilterModel.Orders,
                        Type = "launch",
                    },
                },
            };
        }
    }
}