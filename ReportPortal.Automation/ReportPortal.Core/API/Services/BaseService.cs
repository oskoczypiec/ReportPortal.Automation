// <copyright file="BaseService.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Core.API.Services
{
    using RestSharp;

    public abstract class BaseService
    {
        private RestClient client;

        protected BaseService(string baseUrl)
        {
            this.client = new RestClient(baseUrl);
        }

        public async Task<RestResponse> ExecuteAsync(RestRequest request)
        {
            var response = await this.client.ExecuteAsync(request);
            return response;
        }
    }
}