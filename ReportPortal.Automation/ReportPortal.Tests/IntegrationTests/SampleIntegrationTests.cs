// <copyright file="SampleIntegrationTests.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Tests.IntegrationTests
{
    using ReportPortal.Core.Logger;

    public class SampleIntegrationTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Logger.Log.Info("test test test");
            Assert.Pass();
        }
    }
}