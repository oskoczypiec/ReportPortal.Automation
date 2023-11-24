using ReportPortal.Core.API;

namespace ReportPortal.Tests.API
{
    public class BaseApiTest
    {
        private Endpoints endpoints;

        [OneTimeSetUp] 
        public void Init() 
        { 
            // TODO: prepare data generation - doesnt work
            endpoints = new Endpoints();
            endpoints.GenerateDemoData();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            // TODO: prepare clearing up everything
        }
    }
}
