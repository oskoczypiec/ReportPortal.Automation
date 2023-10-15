// <copyright file="Connection.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Core.API
{
    using ReportPortal.Core.Config;
    using RestSharp;

    public class Connection
    {
        private readonly string? baseUrl;

        public Connection()
        {
            this.baseUrl = ApplicationConfiguration.GetUrl();
        }

        private RestClient? Client { get; set; }

        private RestResponse? Response { get; set; }

        private RestRequest? Request { get; set; }

        public RestResponse GetResponseContent(string url, string? key = null, string? keyValue = null)
        {
            this.Client = Uri.IsWellFormedUriString(url, UriKind.Absolute) ? new RestClient(url) : new RestClient($"{this.baseUrl}{url}");
            this.Request = new RestRequest();

            try
            {
                this.Response = this.Client.ExecuteGetAsync(this.Request).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                this.Response!.ResponseStatus = ResponseStatus.Error;
                this.Response.ErrorMessage = ex.Message;
                this.Response.ErrorException = ex;
            }

            return this.Response;
        }
    }
}
