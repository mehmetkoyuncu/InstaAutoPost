using InstaAutoPost.SendPostBot.Core.Selenium;
using InstaAutoPost.UI.Data.Entities.Concrete;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Threading;

namespace InstaAutoPost.SendPostBot.UI
{
    public class SeleniumMain
    {
        public bool Publish(SourceContent content, string environment,List<SocialMediaAccounts> accounts)
        {
            bool control = false;
            try
            {
                foreach (var item in accounts)
                {
                    InstagramSelenium instagram = new InstagramSelenium();
                    instagram.GoToMainPage();
                    Console.WriteLine("Instagram anasayfası açıldı.");
                    Thread.Sleep(5000);
                    var userName = item.AccountNameOrMail;
                    var password = item.Password;
                    instagram.Login(userName, password);
                    Thread.Sleep(2000);
                    Console.WriteLine("İnstagrama başarı bir şekilde giriş yapıldı.");
                    instagram.SendPost(content, environment);
                    Console.WriteLine("Post başarılı bir şekilde gönderildi.");              
                }
                control = true;
            }
            catch (Exception ex)
            {
                control = false;
            }
            return control;
        }
        static void Main(string[] args)
        {
        }
    }
}
