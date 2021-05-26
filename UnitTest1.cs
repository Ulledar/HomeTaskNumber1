using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace HomeTaskNumber1
{
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

        private readonly string testPageUrl = "https://www.demoblaze.com/index.html";
        private readonly string username = "username";
        private readonly string password = "password";
        #endregion

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Url = "https://www.google.com";
        }

        [Test]
        public void Test1()
        {
            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(3));
            driver.Navigate().GoToUrl(testPageUrl);
            wait.Until(ExpectedConditions.ElementIsVisible(loginButtonMainPage));
            driver.FindElement(loginButtonMainPage).Click();
            IWebElement usernameField = driver.FindElement(usernameInputField);
            wait.Until(ExpectedConditions.ElementIsVisible(usernameInputField));
            usernameField.Clear();
            usernameField.SendKeys(username);
            IWebElement passwordField = driver.FindElement(passwordInputField);
            passwordField.Clear();
            passwordField.SendKeys(password);
            driver.FindElement(loginButton).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(logoutButton));
            Assert.IsTrue(driver.FindElement(logoutButton).Displayed); // "Log out" button present on page
            Assume.That(driver.FindElement(nameShow).Displayed); // "username" present on page
            Assert.AreEqual("Welcome "+ username, driver.FindElement(nameShow).Text); //correct username present
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}