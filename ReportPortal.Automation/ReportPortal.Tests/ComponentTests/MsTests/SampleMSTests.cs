// <copyright file="SampleMSTests.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>


namespace ReportPortal.Tests.ComponentTests
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ReportPortal.Tests;

    [TestClass]
    [assembly: Parallelize(Workers = 4, Scope = ExecutionScope.ClassLevel)]
    public class SampleMSTests : MsTestBase
    {
        [TestMethod]
        public void Test1()
        {
            var expectedUrl = "https://www.google.com/";
            this.driver!.Navigate().GoToUrl(expectedUrl);
            this.driver.Url.Should().Be(expectedUrl);
        }
    }
}