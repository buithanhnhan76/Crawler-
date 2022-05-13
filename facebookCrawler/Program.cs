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

            // Create file
            System.IO.StreamWriter writer = new System.IO.StreamWriter("C:\\Users\\Nhan Bui\\Documents\\Work\\Crawler\\facebookCrawler\\result\\facebookCrawl.csv", false, System.Text.Encoding.UTF8);
            // Title
            writer.WriteLine("{0},{1},{2}","Tên user","Link user","Post content");
            // System.Threading.Thread.Sleep(1000);

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
    
            // get data
            foreach (var content in contentDivs)
            {
                string outerHtml = content.GetAttribute("outerHTML");
                // all crawl data
                string userName, userLink, postContent;
                // get all value between "" of href
                // user name, get string between <strong><span> and </span></strong>
                try{
                    userName = Regex.Match(outerHtml, "<strong><span>(.*?)</span></strong>").Groups[1].Value;
                }
                catch{
                    userName = "";
                }
                // user link
                try{
                    userLink = Regex.Match(outerHtml, "href=\"(.*?)\"").Groups[1].Value;
                    // get user id by using Regex, between user/ and /
                    string userId = Regex.Match(userLink,"user/(.*?)/").Groups[1].Value;
                    userLink = "https://facebook.com/" + userId;
                }
                catch{
                    userLink = "";
                }

                try{
                    postContent = Regex.Match(outerHtml,"<div dir=\"auto\" style=\"text-align: start;\">(.*?)</div>").Groups[1].Value;
                    // in case post content include icon, hash tag, then delete it
                    if(postContent.Contains("<span")){
                            postContent = Regex.Match(outerHtml,"<div dir=\"auto\" style=\"text-align: start;\">(.*?)<span").Groups[1].Value;
                        }
                    // post dont have text content
                    if(postContent.StartsWith("<a")){
                        postContent = "";
                    }
                }
                catch{
                    postContent = "";
                }
                // write
                writer.WriteLine("{0},{1},{2}",userName, userLink, postContent);
                System.Threading.Thread.Sleep(1000);
            }
          
            writer.Close();
        }
    }
}
