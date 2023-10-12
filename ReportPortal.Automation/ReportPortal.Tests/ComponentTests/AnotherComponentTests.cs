// <copyright file="UnitTest1.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using ReportPortal.Core.Tests;

namespace ReportPortal.Tests.ComponentTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class AnotherComponentTests : BaseTest
    {
        [Test]
        public void Test1()
        {
            Assert.AreEqual("http://localhost:8080/ui/#login", Driver.Url);
        }
    }
}