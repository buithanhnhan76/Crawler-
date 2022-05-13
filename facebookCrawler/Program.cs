using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace facebookCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create an instance of Chrome driver
            IWebDriver browser = new ChromeDriver();
            
            //Navigate to facebook.com
            browser.Navigate().GoToUrl("https://facebook.com");
            System.Threading.Thread.Sleep(2000);

            // login facebook
            var username = browser.FindElements(By.CssSelector("input[name='email']"));
            var password = browser.FindElements(By.CssSelector("input[name='pass']"));
            System.Threading.Thread.Sleep(2000);
            username[0].SendKeys("***REMOVED***");
            password[0].SendKeys("***REMOVED***");
            var loginButton = browser.FindElements(By.CssSelector("button[name='login']"));
            loginButton[0].Click();   
            System.Threading.Thread.Sleep(2000);

            // navigate to group facebook
            browser.Navigate().GoToUrl("https://www.facebook.com/groups/606383689949601");
            System.Threading.Thread.Sleep(2000);

            // Initial list
            List<string> listUsersLink = new List<string>();

            // Scroll
            IJavaScriptExecutor js = (IJavaScriptExecutor) browser;
            js.ExecuteScript("window.scrollTo({top: 500,left:0, behavior: 'smooth'})");
            System.Threading.Thread.Sleep(2000);
            js.ExecuteScript("window.scrollTo({top: 1000,left:0, behavior: 'smooth'})");
            System.Threading.Thread.Sleep(2000);
            js.ExecuteScript("window.scrollTo({top: 1500,left:0, behavior: 'smooth'})");
            System.Threading.Thread.Sleep(2000);
            js.ExecuteScript("window.scrollTo({top: 2000,left:0, behavior: 'smooth'})");
            System.Threading.Thread.Sleep(2000);
            js.ExecuteScript("window.scrollTo({top: 2500,left:0, behavior: 'smooth'})");
            System.Threading.Thread.Sleep(2000);
            js.ExecuteScript("window.scrollTo({top: 3000,left:0, behavior: 'smooth'})");
            System.Threading.Thread.Sleep(2000);
            js.ExecuteScript("window.scrollTo({top: 3500,left:0, behavior: 'smooth'})");
            System.Threading.Thread.Sleep(2000);
            js.ExecuteScript("window.scrollTo({top: 4000,left:0, behavior: 'smooth'})");
            System.Threading.Thread.Sleep(2000);
            js.ExecuteScript("window.scrollTo({top: 4500,left:0, behavior: 'smooth'})");
            System.Threading.Thread.Sleep(2000);

            //Select all content divs
            var contentDivs = browser.FindElements(By.XPath("//div[@class='du4w35lb k4urcfbm l9j0dhe7 sjgh65i0']"));
    

            // get links
            foreach (var content in contentDivs)
            {
                string outerHtml = content.GetAttribute("outerHTML");
                // get all value between "" of href
                string userLink = Regex.Match(outerHtml, "href=\"(.*?)\"").Groups[1].Value;
                // get user id by using Regex, between user/ and /
                string userId = Regex.Match(userLink,"user/(.*?)/").Groups[1].Value;
                userLink = "https://facebook.com/" + userId;
                listUsersLink.Add(userLink);                
            }

            // Create file
            System.IO.StreamWriter writer = new System.IO.StreamWriter("C:\\Users\\Nhan Bui\\Documents\\Work\\Crawler\\facebookCrawler\\result\\facebookCrawl.csv", false, System.Text.Encoding.UTF8);
            // Title
            writer.WriteLine("{0},{1}","Tên user","Link user");
            // System.Threading.Thread.Sleep(1000);

            //Go to each user link
            for (int i = 0; i < listUsersLink.Count; i++)
            {
                // Go to user link
                browser.Navigate().GoToUrl(listUsersLink[i]);
                System.Threading.Thread.Sleep(2000);
                

                // Initial varible
                string userName;
                try
                {   
                    var contentUsername = browser.FindElements(By.XPath("//h1[@class='gmql0nx0 l94mrbxd p1ri9a11 lzcic4wl']"));
                    userName = contentUsername[0].Text;
                }
                catch
                {
                    userName = "";
                }    

                // write
                writer.WriteLine("{0},{1}",userName, listUsersLink[i]);
                System.Threading.Thread.Sleep(1000);

            } 
            writer.Close();
        }
    }
}
