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
            // username and password facebook
            username[0].SendKeys("***REMOVED***");
            password[0].SendKeys("***REMOVED***");
            // login button
            var loginButton = browser.FindElements(By.CssSelector("button[name='login']"));
            loginButton[0].Click();   
            System.Threading.Thread.Sleep(2000);

            // navigate to group facebook
            // navigate to Dao Meo
            browser.Navigate().GoToUrl("https://www.facebook.com/groups/606383689949601");
            System.Threading.Thread.Sleep(2000);

            // Create file
            System.IO.StreamWriter writer = new System.IO.StreamWriter("C:\\Users\\Nhan Bui\\Documents\\Work\\Crawler\\facebookCrawler\\result\\facebookCrawl.csv", false, System.Text.Encoding.UTF8);
            // Title
            writer.WriteLine("{0},{1},{2},{3},{4}","Tên user","Link user","Post content","Image content 1", "Image content 2");
            System.Threading.Thread.Sleep(1000);

            // Scroll
            IJavaScriptExecutor js = (IJavaScriptExecutor) browser;
            js.ExecuteScript("window.scrollTo({top: 2500,left:0, behavior: 'smooth'})");
            System.Threading.Thread.Sleep(2000);
            js.ExecuteScript("window.scrollTo({top: 4500,left:0, behavior: 'smooth'})");
            System.Threading.Thread.Sleep(2000);
            js.ExecuteScript("window.scrollTo({top: 6500,left:0, behavior: 'smooth'})");
            System.Threading.Thread.Sleep(2000);
            js.ExecuteScript("window.scrollTo({top: 8500,left:0, behavior: 'smooth'})");
            System.Threading.Thread.Sleep(2000);
            js.ExecuteScript("window.scrollTo({top: 10500,left:0, behavior: 'smooth'})");
            System.Threading.Thread.Sleep(2000);
            js.ExecuteScript("window.scrollTo({top: 12500,left:0, behavior: 'smooth'})");
            System.Threading.Thread.Sleep(2000);
            js.ExecuteScript("window.scrollTo({top: 14500,left:0, behavior: 'smooth'})");
            System.Threading.Thread.Sleep(2000);
            js.ExecuteScript("window.scrollTo({top: 16500,left:0, behavior: 'smooth'})");
            System.Threading.Thread.Sleep(2000);
            js.ExecuteScript("window.scrollTo({top: 18500,left:0, behavior: 'smooth'})");
            System.Threading.Thread.Sleep(2000);
            js.ExecuteScript("window.scrollTo({top: 20500,left:0, behavior: 'smooth'})");
            System.Threading.Thread.Sleep(2000);
            js.ExecuteScript("window.scrollTo({top: 22500,left:0, behavior: 'smooth'})");
            System.Threading.Thread.Sleep(2000);
            js.ExecuteScript("window.scrollTo({top: 24500,left:0, behavior: 'smooth'})");
            System.Threading.Thread.Sleep(2000);
            js.ExecuteScript("window.scrollTo({top: 26500,left:0, behavior: 'smooth'})");
            System.Threading.Thread.Sleep(2000);
            js.ExecuteScript("window.scrollTo({top: 28500,left:0, behavior: 'smooth'})");
            System.Threading.Thread.Sleep(2000);
            js.ExecuteScript("window.scrollTo({top: 30500,left:0, behavior: 'smooth'})");
            System.Threading.Thread.Sleep(2000);
            js.ExecuteScript("window.scrollTo({top: 32500,left:0, behavior: 'smooth'})");
            System.Threading.Thread.Sleep(2000);
            
            //Select all content divs
            var contentDivs = browser.FindElements(By.XPath("//div[@class='du4w35lb k4urcfbm l9j0dhe7 sjgh65i0']"));
    
            // get data
            foreach (var content in contentDivs)
            {
                string outerHtml = content.GetAttribute("outerHTML");
                // all varible
                string userName = "", userLink = "", postContent = "", imageContent1 = "", imageContent2 = "";

                // user name, get string between <strong><span> and </span></strong>
                try{
                    userName = Regex.Match(outerHtml, "<strong><span>(.*?)</span></strong>").Groups[1].Value;
                }
                catch{
                }

                // user link
                try{
                    userLink = Regex.Match(outerHtml, "href=\"(.*?)\"").Groups[1].Value;
                    // get user id by using Regex, between user/ and /
                    string userId = Regex.Match(userLink,"user/(.*?)/").Groups[1].Value;
                    userLink = "https://facebook.com/" + userId;
                }
                catch{
                }

                // post content
                try{
                    // use regex to get post content
                    postContent = Regex.Match(outerHtml,"<div dir=\"auto\" style=\"text-align: start;\">(.*?)</div>").Groups[1].Value;
                    // in case post content include icon, hash tag, then delete it
                    if(postContent.Contains("<span")){
                            postContent = Regex.Match(outerHtml,"<div dir=\"auto\" style=\"text-align: start;\">(.*?)<span").Groups[1].Value;
                        }
                    // post dont have text content
                    if(postContent.StartsWith("<a")){
                        postContent = "";
                    }
                    // replace , with ""
                    postContent = postContent.Replace(",","");
                }
                catch{
                    postContent = "";
                }

                // get image content
                // image has two classes
                var imgs = Regex.Matches(outerHtml,"class=\"i09qtzwb n7fi1qx3 datstx6m pmk7jnqg j9ispegn kr520xx4 k4urcfbm\" referrerpolicy=\"origin-when-cross-origin\" src=\"(.*?)\">");
                if(imgs.Count == 0){
                     try{
                         imgs = Regex.Matches(outerHtml,"class=\"i09qtzwb n7fi1qx3 datstx6m pmk7jnqg j9ispegn kr520xx4 k4urcfbm bixrwtb6\" referrerpolicy=\"origin-when-cross-origin\" src=\"(.*?)\">");
                     }
                     catch{}
                }
                try{
                    imageContent1 = imgs[0].Groups[1].Value;
                    imageContent1 = imageContent1.Replace("amp;","");
                    imageContent2 = imgs[1].Groups[1].Value;
                    imageContent2 = imageContent2.Replace("amp;","");
                }
                catch{}
                // write
                writer.WriteLine("{0},{1},{2},{3},{4}",userName, userLink, postContent,imageContent1,imageContent2);
                System.Threading.Thread.Sleep(1000);
            }
          
            writer.Close();
        }
    }
}
