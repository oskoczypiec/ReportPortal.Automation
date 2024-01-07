// <copyright file="SampleComponentTests.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Tests.ComponentTests
{
    using FluentAssertions;
    using ReportPortal.Tests;

    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class SampleComponentTests : BaseTest
    {
        [Test]
        public void Test1()
        {
            var expectedUrl = "https://www.google.com/";
            this.driver!.Navigate().GoToUrl(expectedUrl);
            this.driver.Url.Should().Be(expectedUrl);
        }
    }
}