using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace shopeeCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create an instance of Chrome driver
            IWebDriver browser = new ChromeDriver();

            // Initial list
            List<string> listProductLink = new List<string>();

            // For testing purpose, only go to page 0
            for (int i = 0; i < 1; i++) 
            {
            //Navigate to website Shopee.vn > Ao thun category
            browser.Navigate().GoToUrl("https://shopee.vn/search?keyword=ao%20thun&page=" + i);
            System.Threading.Thread.Sleep(2000);

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

            //Select all product items by CSS Selector
            var products = browser.FindElements(By.CssSelector("a[data-sqe='link']"));

            // get links
            foreach (var product in products)
            {
                string outerHtml = product.GetAttribute("outerHTML");
                string productLink = Regex.Match(outerHtml, "href=\"(.*?)\"").Groups[1].Value;
                productLink = "https://shopee.vn" + productLink;
                listProductLink.Add(productLink);                
            }

            }

            // Create file
            System.IO.StreamWriter writer = new System.IO.StreamWriter("C:\\Users\\Nhan Bui\\Documents\\Work\\Crawler\\shopeeCrawler\\result\\shopee.csv", false, System.Text.Encoding.UTF8);
            // Title
            writer.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}","Tên sản phẩm","Size","Color","Giá sản phẩm","Ảnh sản phẩm 1","Ảnh sản phẩm 2","Ảnh sản phẩm 3", "Ảnh sản phẩm 4", "Danh mục", "Thương hiệu","Gửi từ","Đánh giá sản phẩm","Link sản phẩm");
            System.Threading.Thread.Sleep(1000);

            //Go to each product link
            for (int i = 0; i < listProductLink.Count; i++)
            {
                //Go to product link
                browser.Navigate().GoToUrl(listProductLink[i]);
                System.Threading.Thread.Sleep(2000);

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

                //Extract product information by CSS Selector
                // string productTitle = browser.FindElements(By.CssSelector(".title"))[0].Text;

                //Extract product brand by CSS Selector then remove redundant data by Regular Expression
                // string productBrand = browser.FindElements(By.CssSelector(".brand-and-author"))[0].GetAttribute("outerHTML");
                // productBrand = Regex.Match(productBrand, "brand\">(.*?)</a>").Groups[1].Value;
                // string productName, productPrice, productCategory, from, brand, productImageLink1, productImageLink2, productImageLink3, productImageLink4, size, colors, productRating;
                

                // Initial varible
                string productName, size, colors,productPrice ,productImageLink1, productImageLink2, productImageLink3, productImageLink4, productCategory, brand, from, productRating;
                try
                {   
                    // product name
                    productName = browser.FindElements(By.CssSelector("._3g8My- span"))[0].Text;
                    // delete , in product name
                    productName = productName.Replace(",","");
                }
                catch
                {
                    // error
                    productName = "";
                }    
                    
                try
                {
                    // product price
                    productPrice = browser.FindElements(By.CssSelector("._2v0Hgx"))[0].Text;
                }
                catch
                {
                    // error
                    productPrice = "";
                }
                try
                {
                    // product category
                    productCategory = browser.FindElements(By.CssSelector("._2572CL"))[3].Text;
                }
                catch
                {
                    // error
                    productCategory = "";
                }
                    
                try
                {
                    // sent from
                    from = browser.FindElement(By.CssSelector("[class ~= '_1pEVDa']:last-of-type div")).Text;
                    // delete ,
                    from = from.Replace(",","");
                }
                catch
                {
                    // error
                    from = "";
                }
                try
                {
                     // brand
                    brand = browser.FindElements(By.CssSelector("._29-l2W"))[0].Text;
                }
                catch
                {
                    // error
                    brand = "";
                }

                try
                {
                    // image link 1
                    productImageLink1 = browser.FindElements(By.CssSelector("._2UWcUi"))[0].GetCssValue("background-image");
                    // delete url("");
                    productImageLink1 = productImageLink1.Substring(5,productImageLink1.Length - (5 + 2));
                }
                catch
                {
                    // error
                    productImageLink1 = "";
                }

                try
                {
                    // image link 2
                    productImageLink2 = browser.FindElements(By.CssSelector("._2UWcUi"))[1].GetCssValue("background-image");
                    // delete url("");
                    productImageLink2 = productImageLink2.Substring(5,productImageLink2.Length- (5 + 2));
                }
                catch
                {
                    // error
                    productImageLink2 = "";
                }

                try
                {
                    // image link 3
                    productImageLink3 = browser.FindElements(By.CssSelector("._2UWcUi"))[2].GetCssValue("background-image");
                    // delete url("")
                    productImageLink3 = productImageLink3.Substring(5,productImageLink3.Length - ( 5+ 2));
                }
                catch
                {
                    // error
                    productImageLink3 = "";
                }

                try
                {
                    // image link 4
                    productImageLink4 = browser.FindElements(By.CssSelector("._2UWcUi"))[3].GetCssValue("background-image");
                    // delete url("")
                    productImageLink4 = productImageLink4.Substring(5,productImageLink4.Length - (5 + 2));
                }
                catch
                {
                    // error
                    productImageLink4 = "";
                }
                try
                {
                    // size
                    size = browser.FindElements(By.CssSelector("._3ABAc7"))[1].Text;
                    // format size
                    size = size.Replace("\r\n"," ");
                }
                catch
                {
                    // error
                    size = "";
                }
                try
                {
                    // color
                    colors = browser.FindElements(By.CssSelector("._3ABAc7"))[0].Text;
                    // format color
                    colors = colors.Replace("\r\n"," ");
                }
                catch
                {
                    // color
                    colors = "";
                }

                try
                {
                    // rating
                    productRating = browser.FindElements(By.CssSelector(".product-rating-overview__score-wrapper"))[0].Text;
                }
                catch
                {
                    // error
                    productRating = "";
                }
                // try 
                // {
                //     productDesc = browser.FindElements(By.CssSelector(".Mhqp_x"))[0].Text;
                //     productDesc = productDesc.Replace(",","");
                //     productDesc = productDesc.Replace("/r/n","");
                // }
                // catch
                // {
                //     productDesc = "";
                // }

                // writer.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}",productName,productPrice,productCategory,from,brand,listProductLink[i],productImageLink1,productImageLink2,productImageLink3,productImageLink4,size,colors,productRating);
                writer.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}",productName, size, colors,productPrice ,productImageLink1, productImageLink2, productImageLink3, productImageLink4, productCategory, brand, from ,productRating, listProductLink[i]);
                // var line = String.Format("{0}",productName);
                // writer.WriteLine(line);
                writer.Flush();
                System.Threading.Thread.Sleep(1000);
                //Extract product price

                //Extract product images

                //Extract colors

                //Extract sizes

                //Extract product details

                //Extract product description

                System.Threading.Thread.Sleep(5000);
            }

            // Close file
            writer.Close();

            //Console.WriteLine(products.Count);
            //System.IO.StreamWriter writer = new System.IO.StreamWriter("D:\\tiki.csv", false, System.Text.Encoding.UTF8);
            //writer.WriteLine("ProductName\tImageLink");
            ////System.Threading.Thread.Sleep(10000);
            ////string productLink = product.GetAttribute("href");
            ////string productName = product.FindElement(By.CssSelector(".product-item .name")).Text;
            ////string innerHtml = product.GetAttribute("innerHTML");
            //string productName = Regex.Match(outerHtml, "alt=\"(.*?)\"").Groups[1].Value;
            //string productThumbnail = Regex.Match(outerHtml, "<img src=\"(.*?)\"").Groups[1].Value;
            //writer.WriteLine(productName + "\t" + productThumbnail);
            //writer.Close();

            //browser.FindElements(By.CssSelector(".title"))[0].Text;
            //browser.FindElements(By.CssSelector(".title"))[0].GetAttribute("");

        }
    }
}

//browser.FindElements(By.XPath(""));
//browser.FindElement(By.CssSelector(""));
//browser.FindElement(By.XPath(""));