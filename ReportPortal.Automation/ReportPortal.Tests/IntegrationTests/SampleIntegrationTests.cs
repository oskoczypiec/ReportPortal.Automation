// <copyright file="Tests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ReportPortal.Tests.IntegrationTests
{
    using ReportPortal.Core.Logger;
    using ReportPortal.Core.Tests;

    /// <summary>
    /// Example tests.
    /// </summary>
    public class SampleIntegrationTests
    {
        /// <summary>
        /// Preconditions before test run.
        /// </summary>
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Example test.
        /// </summary>
        [Test]
        public void Test1()
        {
            Logger.Log.Info("test test test");
            Assert.Pass();
        }
    }
}