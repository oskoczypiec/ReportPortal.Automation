using ReportPortal.Core.API.Models;

namespace ReportPortal.Tests.API.Mock
{
    public interface IFilterApiClient
    {
        public Task<FiltersRootModel> GetFiltersAsync();
    }
}
