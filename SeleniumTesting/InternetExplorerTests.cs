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
            driver.Navigate().GoToUrl("http://www.google.com/ncr");
            Task.Delay(TimeSpan.FromSeconds(10)).Wait();

            var screenshot = driver.GetScreenshot();
            screenshot.SaveAsFile("ie-snapshot.png", ImageFormat.Png);

            //IWebElement query = driver.GetElement(By.Name("q"));
            //query.SendKeys("Selenium");
            //query.Submit();
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            //wait.Until((d) => { return d.Title.StartsWith("Selenium"); });

            //Assert.Equal("Selenium - Google Search", driver.Title);
        }
    }

    public class InternetExplorerFixture : IDisposable
    {
        InternetExplorerDriver driver;

        public InternetExplorerFixture()
        {
            DesiredCapabilities caps = DesiredCapabilities.InternetExplorer();
            caps.SetCapability("ignoreZoomSetting", true);
            driver = new InternetExplorerDriver(new InternetExplorerOptions { IgnoreZoomLevel = true });
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
