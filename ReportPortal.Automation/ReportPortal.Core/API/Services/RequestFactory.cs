// <copyright file="RequestFactory.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Core.API.Services
{
    using Newtonsoft.Json;
    using ReportPortal.Core.API.Models;
    using ReportPortal.Core.Config;
    using RestSharp;

    public class RequestFactory
    {
        public RequestFactory()
        {
            ApplicationConfiguration.SetUp();
            this.Client = new RestClient(ApplicationConfiguration.GetBaseUrl());
        }

        private RestClient? Client { get; set; }

        public RestRequest GetRequest(string resource, Method method)
        {
            var responseGetUserToken = this.GetUserToken().Result;
            var token = JsonConvert.DeserializeObject<GetAuthModel>(responseGetUserToken.Content!) !.AccessToken;
            var request = new RestRequest(resource, method);
            request.AddHeader("Authorization", $"Bearer {token}");
            return request;
        }

        public async Task<RestResponse> GetUserToken()
        {
            var request = new RestRequest("/uat/sso/oauth/token", Method.Post)
                .AddHeader("Authorization", "Basic dWk6dWltYW4=")
                .AddParameter("username", "default")
                .AddParameter("password", Settings.Pass)
                .AddParameter("grant_type", "password");

            RestResponse response = await this.Client!.ExecuteAsync(request);
            return response;
        }
    }
}
