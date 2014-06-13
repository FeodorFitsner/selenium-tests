using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SeleniumTesting
{
    public class WebDriverTests
    {
        //[Fact]
        public void FireFoxTest()
        {
            FirefoxDriver driver = new FirefoxDriver();

            driver.Navigate().GoToUrl("http://www.google.com/ncr");
            IWebElement query = driver.GetElement(By.Name("q"));
            query.SendKeys("Selenium");
            query.Submit();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until((d) => { return d.Title.StartsWith("Selenium"); });

            try
            {
                Assert.Equal("Selenium - Google Search", driver.Title);
            }
            finally
            {
                driver.Quit();
            }
        }

        [Fact]
        public void ChromeTest()
        {
            ChromeDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl("http://www.google.com/ncr");
            IWebElement query = driver.GetElement(By.Name("q"));
            query.SendKeys("Selenium");
            query.Submit();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until((d) => { return d.Title.StartsWith("Selenium"); });

            try
            {
                Assert.Equal("Selenium - Google Search", driver.Title);
            }
            finally
            {
                driver.Quit();
            }
        }

        //[Fact]
        public void InternetExplorerTest()
        {
            DesiredCapabilities caps = DesiredCapabilities.InternetExplorer();
            caps.SetCapability("ignoreZoomSetting", true);
            InternetExplorerDriver driver = new InternetExplorerDriver(new InternetExplorerOptions { IgnoreZoomLevel = true });

            driver.Navigate().GoToUrl("http://www.google.com/ncr");
            IWebElement query = driver.GetElement(By.Name("q"));
            query.SendKeys("Selenium");
            query.Submit();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until((d) => { return d.Title.StartsWith("Selenium"); });

            try
            {
                Assert.Equal("Selenium - Google Search", driver.Title);
            }
            finally
            {
                driver.Quit();
            }
        }
    }

    public static class WebDriverExtensions
    {
        public static SelectElement GetSelectElement(this IWebDriver driver, By by)
        {
            return new SelectElement(driver.GetElement(by));
        }
        public static IWebElement GetElement(this IWebDriver driver, By by)
        {
            for (int i = 1; i <= 5; i++)
            {
                try
                {
                    return driver.FindElement(by);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception was raised on locating element: " + e.Message);
                }
            }
            throw new ElementNotVisibleException(by.ToString());
        }
    }
}
