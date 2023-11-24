using ReportPortal.Core.API.Models;
using ReportPortal.Core.Utilities;

namespace ReportPortal.Business.DataSets
{
    public class FiltersDataProvider
    {
        public FiltersRoot GenerateDefaultGetFiltersResponse()
        {
            return new FiltersRoot()
            {
                Content = new List<Content>()
                {
                    new Content()
                    {
                        Owner = "default",
                        Id = 22,
                        Name = "DEMO_FILTER",
                        Orders = new List<Order>()
                        {
                            new Order()
                            {
                                IsAsc = false,
                                SortingColumn = "startTime",
                            },
                        },
                        Conditions = new List<Condition>()
                        {
                            new Condition()
                            {
                                ConditionText = "has",
                                FilteringField = "compositeAttribute",
                                Value = "demo",
                            },
                        },
                        Type = "Launch",
                    },
                },
                Page = new Page()
                {
                    Number = 1,
                    Size = 20,
                    TotalElements = 1,
                    TotalPages = 1,
                },
            };
        }

        public FiltersRoot GenerateRandomGetFiltersResponse()
        {
            return new FiltersRoot()
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
    }
}
