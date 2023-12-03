// <copyright file="ErrorMessages.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

using ReportPortal.Client.Abstractions.Responses;

namespace ReportPortal.Core.Data
{
    public static class ErrorMessages
    {
        public static string IncorrectFilterType => "Incorrect filtering parameters. ";

        public static string EmptyFiltersError => "should not contain only white spaces and shouldn't be empty.";

        public static string FilterNotFoundError(string id, string projectName = "default_personal") => $"User filter with ID '{id}' not found on project '{projectName}'. Did you use correct User Filter ID?";
    }
}
