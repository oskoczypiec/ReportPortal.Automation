// <copyright file="Endpoints.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Core.API
{
    using ReportPortal.Core.Data;
    using RestSharp;

    public class Endpoints
    {
        private Connection _connection;

        public Endpoints()
        { 
            _connection = new Connection();
        }

        public async Task<RestResponse> GetFilter()
        {
            return await _connection.GetResponseContent(FiltersEndpoint.Filter);
        }

        public async Task<RestResponse> GenerateDemoData()
        {
            return await _connection.PostResponseContent(GenericEndpoints.GenerateDemoData);
        }
    }
}
