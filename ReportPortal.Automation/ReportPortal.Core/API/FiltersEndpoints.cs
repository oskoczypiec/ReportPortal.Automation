﻿// <copyright file="Endpoints.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Core.API
{
    using Newtonsoft.Json;
    using ReportPortal.Core.API.Models;
    using ReportPortal.Core.API.Services;
    using ReportPortal.Core.Config;
    using ReportPortal.Core.Data;
    using RestSharp;
    using System;

    public class FiltersEndpoints : BaseService
    {
        private static string BaseUrl => ApplicationConfiguration.GetBaseUrl();

        public FiltersEndpoints()
            : base(BaseUrl)
        {
        }

        public async Task<RestResponse> GetFilter(string project = "default_personal")
        {
            var request = new RequestFactory().GetRequest(FiltersEndpoint.FilterByProject(project), Method.Get);

            var response = await this.ExecuteAsync(request);
            return response;
        }

        public async Task<RestResponse> DeleteLaunchByIds(LaunchRequestModel requestModel, string project = "default_personal")
        {
            var request = new RequestFactory().GetRequest(LaunchEndpoint.GetAllLaunchesByProjectName(project), Method.Delete);
            var jsonBody = JsonConvert.SerializeObject(requestModel);
            request.AddJsonBody(jsonBody);

            var response = await this.ExecuteAsync(request);
            return response;
        }

        public async Task<RestResponse> DeleteDashboardById(string id, string project = "default_personal")
        {
            var request = new RequestFactory().GetRequest(DashboardEndpoint.DeleteDashboardByProjectNameAndId(id, project), Method.Delete);

            var response = await this.ExecuteAsync(request);
            return response;
        }

        public async Task<RestResponse> GenerateDemoData(string project = "default_personal")
        {
            var request = new RequestFactory().GetRequest(GenericEndpoints.GenerateDemoData(project), Method.Post);
            var jsonBody = "{}";
            request.AddJsonBody(jsonBody);

            var response = await this.ExecuteAsync(request);
            return response;
        }

        public async Task<RestResponse> PostFilter(NewFilterModel filterModel, string project = "default_personal")
        {
            var request = new RequestFactory().GetRequest(FiltersEndpoint.FilterByProject(project), Method.Post);
            var jsonBody = JsonConvert.SerializeObject(filterModel);
            request.AddJsonBody(jsonBody);

            var response = await this.ExecuteAsync(request);
            return response;
        }

        public async Task<RestResponse> PutFilterById(NewFilterModel filterModel, string id, string project = "default_personal")
        {
            var request = new RequestFactory().GetRequest(FiltersEndpoint.FilterById(id, project), Method.Put);
            var jsonBody = JsonConvert.SerializeObject(filterModel);
            request.AddJsonBody(jsonBody);

            var response = await this.ExecuteAsync(request);
            return response;
        }

        public async Task<RestResponse> DeleteFiltersById(int id, string project = "default_personal")
        {
            var request = new RequestFactory().GetRequest(FiltersEndpoint.DeleteFilterByProjectNameAndId(id, project), Method.Delete);

            var response = await this.ExecuteAsync(request);
            return response;
        }

        public async Task<RestResponse> GetFilterNames(string project = "default_personal")
        {
            var request = new RequestFactory().GetRequest(FiltersEndpoint.FilterNames(project), Method.Get);

            var response = await this.ExecuteAsync(request);
            return response;
        }
    }
}
