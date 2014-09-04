using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SeleniumTesting
{
    public class ChromeTests : IUseFixture<ChromeFixture>
    {
        ChromeDriver driver;

        public void SetFixture(ChromeFixture data)
        {
            driver = data.GetDriver();
        }

        //[Fact]
        public void Google_com_should_return_search_results()
        {
            driver.Navigate().GoToUrl("http://www.google.com/ncr");
            Task.Delay(TimeSpan.FromSeconds(5)).Wait();

            // here you can check HTML of the page you currently have loaded in the browser
            // and save it to the file
            File.WriteAllText("chrome-source-1.html", driver.PageSource);

            IWebElement query = driver.FindElement(By.Name("q"));
            query.SendKeys("Selenium");
            query.Submit();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until((d) => { return d.Title.StartsWith("Selenium"); });

            Assert.Equal("Selenium - Google Search", driver.Title);

            driver.GetScreenshot().SaveAsFile("chrome-snapshot.png", ImageFormat.Png);
        }
    }

    public class ChromeFixture : IDisposable
    {
        ChromeDriver driver;

        public ChromeFixture()
        {
            driver = new ChromeDriver();
        }

        public ChromeDriver GetDriver()
        {
            return driver;
        }

        public void Dispose()
        {
            driver.Quit();
        }
    }
}
