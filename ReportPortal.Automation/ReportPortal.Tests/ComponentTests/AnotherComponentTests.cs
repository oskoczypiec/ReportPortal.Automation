// <copyright file="AnotherComponentTests.cs" company="EPAM">
// Copyright (c) EPAM. All rights reserved.
// </copyright>

namespace ReportPortal.Tests.ComponentTests
{
    using FluentAssertions;
    using ReportPortal.Business.Pages;
    using ReportPortal.Core.Config;
    using ReportPortal.Core.Tests;
    using ReportPortal.Core.Utilities;

    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class AnotherComponentTests : BaseTest
    {
        private LoginPage loginPage;
        private string applicationUrl;

        [SetUp]
        public void Setup()
        {
            this.loginPage = new LoginPage(this.driver!);
            this.applicationUrl = ApplicationConfiguration.GetUrl();
        }

        [Test]
        public void Login_Failure_When_Username_Password_Incorrect()
        {
            // Assemble
            var randomEmail = RandomHelper.RandomEmailAddress();
            var randomPassword = RandomHelper.RandomPassword();
            var expectedUrl = $"{this.applicationUrl}/ui/#login";

            // Act
            this.loginPage.SetUserName(randomEmail);
            this.loginPage.PressTabOnUserNameInput();
            this.loginPage.SetPassword(randomPassword);
            this.loginPage.ClickLoginButton();

            // Assert
            this.driver!.Url.Should().Be(expectedUrl);
        }
    }
}