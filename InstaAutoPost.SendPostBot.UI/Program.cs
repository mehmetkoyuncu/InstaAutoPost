using InstaAutoPost.SendPostBot.Core.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace InstaAutoPost.SendPostBot.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            InstagramSelenium instagram = new InstagramSelenium();
            instagram.GoToMainPage();
            Console.WriteLine("Instagram anasayfası açıldı.");
            Thread.Sleep(5000);
            var userName = "haberdarmaymun";
            var password = "4421751mk";
            instagram.Login(userName,password);
            Thread.Sleep(2000);
            Console.WriteLine("İnstagrama başarı bir şekilde giriş yapıldı.");
            instagram.SendPost();
            Console.WriteLine("Post başarılı bir şekilde gönderildi.");
            Console.ReadKey();
          

        }
    }
}
