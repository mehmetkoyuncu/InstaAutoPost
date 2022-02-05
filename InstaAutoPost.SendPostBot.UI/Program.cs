using InstaAutoPost.SendPostBot.Core.Selenium;
using InstaAutoPost.UI.Data.Entities.Concrete;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace InstaAutoPost.SendPostBot.UI
{
    public class SeleniumMain
    {
        public bool Publish(SourceContent content,string environment)
        {
            bool control = false;
            string[] nullableStringArray=null;
            try
            {
                InstagramSelenium instagram = new InstagramSelenium();
                instagram.GoToMainPage();
                Console.WriteLine("Instagram anasayfası açıldı.");
                Thread.Sleep(5000);
                var userName = "haberdarmaymun";
                var password = "4421751mk";
                instagram.Login(userName, password);
                Thread.Sleep(2000);
                Console.WriteLine("İnstagrama başarı bir şekilde giriş yapıldı.");
                instagram.SendPost(content,environment);
                Console.WriteLine("Post başarılı bir şekilde gönderildi.");
                Console.ReadKey();
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
