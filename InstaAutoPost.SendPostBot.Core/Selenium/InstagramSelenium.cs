
using InstaAutoPost.UI.Data.Entities.Concrete;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Opera;
using System;
using System.Collections.Generic;
using System.IO;
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
            var a = options.BrowserVersion;
            options.AddAdditionalCapability("useAutomationExtension", false);
            options.AddExcludedArgument("enable-automation");
            var directory= Path.Combine( Directory.GetCurrentDirectory(),"drivers");
            _driver = new ChromeDriver(directory,options);
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
            Thread.Sleep(1000);
            passwordWeb.SendKeys(password);
            Thread.Sleep(1000);
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
            Thread.Sleep(5000);
            _driver.FindElement(By.XPath("//button[contains(text(), 'Next')]")).Click();
            Thread.Sleep(1000);
            _driver.FindElement(By.XPath("//button[contains(text(), 'Next')]")).Click();
            Thread.Sleep(1000);
            if (content.Description.Length >= 2000)
            {
                content.Description = content.Description.Substring(0, 2000);
                int lastDot=content.Description.LastIndexOf('.');
                content.Description = content.Description.Substring(0, lastDot);
            }

          

            var tags = "#" + content.Tags + " #" + content.Category.Tags;
            var newTags = tags.Replace(",", " #");
            var fullContent = $"{content.Description} \n\n Kaynak: {content.Category.Source.Name} \n\n {newTags}";
            _driver.FindElement(By.TagName("textarea")).SendKeys(fullContent);
            Thread.Sleep(5000);
            _driver.FindElement(By.XPath("//button[contains(text(), 'Share')]")).Click();
            Thread.Sleep(10000);
            _driver.Close();


        }
    }
}
