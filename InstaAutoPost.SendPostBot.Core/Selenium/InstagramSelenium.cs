
using InstaAutoPost.UI.Data.Entities.Concrete;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TextCopy;

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
        public void SendPost(SourceContent content,string environment)
        {
            var contentRooth = environment + @"\wwwroot\images\" + content.imageURL;
            Thread.Sleep(10000);
            var buttons = _driver.FindElements(By.XPath("//button[@type='button']"));
            buttons.Last().Click();
            Thread.Sleep(2000);
            _driver.FindElement(By.XPath("//form[@role='presentation']//input")).SendKeys(contentRooth);

           
        }
    }
}
