// <copyright file="RequestFactory.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Core.API.Services
{
    using Newtonsoft.Json;
    using ReportPortal.Core.API.Models;
    using ReportPortal.Core.Config;
    using RestSharp;
    using ReportPortal.Core.Logger;

    public class RequestFactory
    {
        private RestClient? Client { get; set; }

        private RestRequest? Request { get; set; }

        private readonly string? baseUrl;

        public RequestFactory()
        {
            ApplicationConfiguration.SetUp();
            baseUrl = ApplicationConfiguration.GetBaseUrl();
            Client = new RestClient(ApplicationConfiguration.GetBaseUrl());
        }

        public RestRequest GetRequest(string resource, Method method)
        {
            var responseGetUserToken = this.GetUserToken().Result;
            Logger.Log.Info($"responseGetUserToken: {responseGetUserToken.Content}");

            var token = JsonConvert.DeserializeObject<GetAuthModel>(responseGetUserToken.Content!) !.AccessToken;
            var request = new RestRequest(resource, method);
            request.AddHeader("Authorization", $"Bearer {token}");
            return request;
        }

        public async Task<RestResponse> GetUserToken()
        {
            Logger.Log.Info($"password :{Settings.Pass}");

            var request = new RestRequest("/uat/sso/oauth/token", Method.Post)
                .AddHeader("Authorization", "Basic dWk6dWltYW4=")
                .AddParameter("username", "default")
                .AddParameter("password", Settings.Pass)
                .AddParameter("grant_type", "password");

            RestResponse response = Client.Execute(request);
            Logger.Log.Info($"request: {request}");
            Logger.Log.Info($"response: {response.ErrorMessage}");

            return response;
        }
    }
}
