// <copyright file="UnitTest1.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using OpenQA.Selenium;
using ReportPortal.Core.Config;
using ReportPortal.Core.Tests;

namespace ReportPortal.Tests.ComponentTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class SampleComponentTests : BaseTest
    {

        [Test]
        public void Test1()
        {
            this.Driver.Navigate().GoToUrl("https://www.google.com/");
            Assert.AreEqual("https://www.google.com/", Driver.Url);
        }
    }
}