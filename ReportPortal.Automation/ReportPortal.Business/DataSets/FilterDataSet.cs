// <copyright file="FilterDataSet.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Business.DataSets
{
    public static class FilterDataSet
    {
        public static IEnumerable<object[]> DifferentFilters
        {
            get
            {
                return new[]
                {
                    new object[] { FilterNames.Passed, "30", 1 },
                    new object[] { FilterNames.Skipped, "1", 2 },
                    new object[] { FilterNames.Description, "Demo", 5 },
                    new object[] { FilterNames.Failed, "1", 4 },
                    new object[] { FilterNames.TotalToInvestigate, "1", 4 },
                };
            }
        }
    }
}
