
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace InstaAutoPost.SendPostBot.Core.Selenium
{
    public class InstagramSelenium
    {
        private IWebDriver _driver;
        public InstagramSelenium()
        {

            
            ChromeOptions options = new ChromeOptions();
            options.AddAdditionalCapability("useAutomationExtension", false);
            options.AddExcludedArgument("enable-automation");
            _driver = new ChromeDriver(options);
        }
        public void GoToMainPage()
        {
            _driver.Navigate().GoToUrl("https://www.instagram.com/");

        }
        public void Login(string username,string password)
        {
            IWebElement usernameWeb = _driver.FindElement(By.Name("username"));
            IWebElement passwordWeb = _driver.FindElement(By.Name("password"));
            usernameWeb.SendKeys(username);
            passwordWeb.SendKeys(password);
            usernameWeb.SendKeys(Keys.Enter);
        }
        public void SendPost()
        {
            Thread.Sleep(10000);
            var buttons = _driver.FindElements(By.XPath("//button[@type='button']"));
            buttons.Last().Click();
            Thread.Sleep(2000);
            var sendPostButton = _driver.FindElement(By.XPath("//button[contains(text(),'Select from computer')]"));
            sendPostButton.SendKeys(@"C:\images\fener.jpg");
            sendPostButton.Click();
            _driver.FindElement(By.XPath("//button[contains(text(),'Select from computer')]")).SendKeys(@"C:\MyFiles\Test.jpg");

           
        }
    }
}
