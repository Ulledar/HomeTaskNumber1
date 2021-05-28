using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace HomeTaskNumber1
{
    [TestFixture]
    public class Tests
    {
        private IWebDriver driver;

        #region Locators
        private readonly By loginButtonMainPage = By.Id("login2");
        private readonly By usernameInputField = By.CssSelector("input#loginusername");  //By.Id("loginusername");
        private readonly By passwordInputField = By.XPath("//input[@id='loginpassword']");  //By.Id("loginpassword");
        private readonly By loginButton = By.XPath("//button[contains(text(), 'Log in')]");
        private readonly By logoutButton = By.CssSelector("#logout2");
        private readonly By nameShow = By.XPath("//a[@id='nameofuser']");
        #endregion

        private readonly string testPageUrl = "https://www.demoblaze.com/index.html";
        private readonly string username = "username";
        private readonly string password = "password";

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Url = "https://www.google.com";
        }
        
        [Test]
        [Author("AlexGrech")]
        [Category("Test case ID: 1")]
        [Description("Verify that it is possible to login with valid credentials")]
        public void Test1()
        {
            WebDriverWait wait = new(driver, new TimeSpan(0, 0, 10));
            driver.Navigate().GoToUrl(testPageUrl);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(loginButtonMainPage));
            driver.FindElement(loginButtonMainPage).Click();
            IWebElement usernameField = driver.FindElement(usernameInputField);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(usernameInputField));
            usernameField.Clear();
            usernameField.SendKeys(username);
            IWebElement passwordField = driver.FindElement(passwordInputField);
            passwordField.Clear();
            passwordField.SendKeys(password);
            driver.FindElement(loginButton).Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(logoutButton));
            Assert.IsTrue(driver.FindElement(logoutButton).Displayed, "No button");
            Assume.That(driver.FindElement(nameShow).Displayed);
            Assert.AreEqual("Welcome "+ username, driver.FindElement(nameShow).Text, "No name field");
        }

        [Ignore("alternative example with TestCase")]
        [TestCase("username", "password")]
        [Author("AlexGrech")]
        [Category("Test case ID: 1")]
        [Description("Verify that it is possible to login with valid credentials")]
        public void Test2(string name, string pass)
        {
            WebDriverWait wait = new(driver, new TimeSpan(0, 0, 10));
            driver.Navigate().GoToUrl(testPageUrl);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(loginButtonMainPage));
            driver.FindElement(loginButtonMainPage).Click();
            IWebElement usernameField = driver.FindElement(usernameInputField);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(usernameInputField));
            usernameField.Clear();
            usernameField.SendKeys(name);
            IWebElement passwordField = driver.FindElement(passwordInputField);
            passwordField.Clear();
            passwordField.SendKeys(pass);
            driver.FindElement(loginButton).Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(logoutButton));
            Assert.IsTrue(driver.FindElement(logoutButton).Displayed, "No button");
            Assume.That(driver.FindElement(nameShow).Displayed);
            Assert.AreEqual("Welcome " + username, driver.FindElement(nameShow).Text, "No name field");
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}