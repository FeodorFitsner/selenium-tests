using OpenQA.Selenium;
using OpenQA.Selenium.IE;
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
    public class InternetExplorerTests : IUseFixture<InternetExplorerFixture>
    {
        InternetExplorerDriver driver;

        public void SetFixture(InternetExplorerFixture data)
        {
            driver = data.GetDriver();
        }

        [Fact]
        public void Google_com_should_return_search_results()
        {
            try
            {
                driver.Navigate().GoToUrl("http://www.google.com/ncr");
                Task.Delay(TimeSpan.FromSeconds(5)).Wait();

                IWebElement query = driver.GetElement(By.Name("q"));
                query.SendKeys("Selenium");
                query.Submit();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                wait.Until((d) => { return d.Title.StartsWith("Selenium"); });

                Assert.Equal("Selenium - Google Search", driver.Title);

                driver.GetScreenshot().SaveAsFile("ie-snapshot.png", ImageFormat.Png);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

    public class InternetExplorerFixture : IDisposable
    {
        InternetExplorerDriver driver;

        public InternetExplorerFixture()
        {
            var service = InternetExplorerDriverService.CreateDefaultService();
            service.LogFile = "ie-log.txt";
            service.LoggingLevel = InternetExplorerDriverLogLevel.Debug;

            driver = new InternetExplorerDriver(service, new InternetExplorerOptions
            {
                IgnoreZoomLevel = true,
                ForceShellWindowsApi = true
            });
        }

        public InternetExplorerDriver GetDriver()
        {
            return driver;
        }

        public void Dispose()
        {
            driver.Quit();
        }
    }
}
