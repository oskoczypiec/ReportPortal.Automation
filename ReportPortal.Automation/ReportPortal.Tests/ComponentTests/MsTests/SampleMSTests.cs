// <copyright file="SampleMSTests.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;

[assembly: Parallelize(Workers = 4, Scope = ExecutionScope.ClassLevel)]

namespace ReportPortal.Tests.ComponentTests.MsTest
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ReportPortal.Tests;

    [TestClass]
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