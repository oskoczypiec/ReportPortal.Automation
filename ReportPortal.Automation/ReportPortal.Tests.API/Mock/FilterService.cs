using ReportPortal.Core.API.Models;

namespace ReportPortal.Tests.API.Mock
{
    public class FilterService
    {
        private readonly IFilterApiClient _filterApi;

        public FilterService(IFilterApiClient filterApi)
        {
            _filterApi = filterApi;
        }

        public Task<FiltersRootModel> GetFilters()
        {
            // Your implementation that calls the filter API
            return _filterApi.GetFiltersAsync();
        }
    }
}

