// <copyright file="Connection.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Core.API
{
    using Newtonsoft.Json;
    using ReportPortal.Core.API.Models;
    using ReportPortal.Core.Config;
    using RestSharp;

    public class Connection
    {
        private readonly string? baseUrl;

        public Connection()
        {
            ApplicationConfiguration.SetUp();
            this.baseUrl = ApplicationConfiguration.GetBaseUrl();
            this.Client = new RestClient(ApplicationConfiguration.GetBaseUrl());
        }

        private RestClient? Client { get; set; }

        private RestRequest? Request { get; set; }

        public async Task<RestResponse> GetUserToken()
        {
            var request = new RestRequest("/uat/sso/oauth/token", Method.Post)
                .AddHeader("Authorization", "Basic " + "dWk6dWltYW4=")
                .AddParameter("username", Settings.User)
                .AddParameter("password", Settings.Pass)
                .AddParameter("grant_type", "password");

            RestResponse response = this.Client.Execute(request);

            return response;
        }

        public Task<RestResponse> GetResponseContent(string url)
        {
            var responseGetUserToken = GetUserToken().Result;
            var token = JsonConvert.DeserializeObject<GetAuthModel>(responseGetUserToken.Content).AccessToken;
            this.Client = Uri.IsWellFormedUriString(url, UriKind.Absolute) ? new RestClient(url) : new RestClient($"{this.baseUrl}{url}");
            this.Request = new RestRequest();
            this.Request.AddHeader("Authorization", $"Bearer {token}");

            var response = this.Client.ExecuteGetAsync(this.Request);

            return response;
        }

        public Task<RestResponse> PostResponseContent(string url, object? body = null)
        {
            var responseGetUserToken = GetUserToken().Result;
            var requestBody = body == null ? "{}" : body;
            var token = JsonConvert.DeserializeObject<GetAuthModel>(responseGetUserToken.Content).AccessToken;
            this.Client = Uri.IsWellFormedUriString(url, UriKind.Absolute) ? new RestClient(url) : new RestClient($"{this.baseUrl}{url}");
            this.Request = new RestRequest();
            this.Request.AddHeader("Authorization", $"Bearer {token}");
            this.Request.AddBody(requestBody);
            var response = this.Client.ExecutePostAsync(this.Request);

            return response;
        }

        public Task<RestResponse> ExecuteRequest(Method method, string url, object? body = null)
        {
            var responseGetUserToken = GetUserToken().Result;
            var requestBody = body == null ? "{}" : body;
            var token = JsonConvert.DeserializeObject<GetAuthModel>(responseGetUserToken.Content).AccessToken;
            this.Client = Uri.IsWellFormedUriString(url, UriKind.Absolute) ? new RestClient(url) : new RestClient($"{this.baseUrl}{url}");
            this.Request = new RestRequest();
            this.Request.AddHeader("Authorization", $"Bearer {token}");
            this.Request.AddBody(requestBody);
            var response = this.Client.ExecuteAsync(this.Request, method);

            return response;
        }
    }
}
