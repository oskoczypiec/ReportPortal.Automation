﻿using Newtonsoft.Json;
using ReportPortal.Core.API.Models;
using ReportPortal.Core.Config;
using RestSharp;

namespace ReportPortal.Core.API.Services
{
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
            var responseGetUserToken = GetUserToken().Result;
            var token = JsonConvert.DeserializeObject<GetAuthModel>(responseGetUserToken.Content).AccessToken;

            var request = new RestRequest(resource, method);
            request.AddHeader("Authorization", $"Bearer {token}");
            return request;
        }

        public async Task<RestResponse> GetUserToken()
        {
            var request = new RestRequest("/uat/sso/oauth/token", Method.Post)
                .AddHeader("Authorization", "Basic dWk6dWltYW4=")
                .AddParameter("username", Settings.User)
                .AddParameter("password", Settings.Pass)
                .AddParameter("grant_type", "password");

            RestResponse response = Client.Execute(request);

            return response;
        }
    }
}
