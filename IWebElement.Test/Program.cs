using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;

namespace FunkyBDD.SxS.Selenium.WebElement.Test
{
    internal static class Program
    {
        public static IWebDriver Driver;

        static void Main()
        {
            #region init browser
                var firefoxOptions = new FirefoxOptions();
                firefoxOptions.SetLoggingPreference(LogType.Browser, LogLevel.Off);
                firefoxOptions.SetLoggingPreference(LogType.Server, LogLevel.Off);
                firefoxOptions.SetLoggingPreference(LogType.Client, LogLevel.Off);
                firefoxOptions.SetLoggingPreference(LogType.Profiler, LogLevel.Off);
                firefoxOptions.SetLoggingPreference(LogType.Driver, LogLevel.Off);
                firefoxOptions.LogLevel = FirefoxDriverLogLevel.Error;
                firefoxOptions.AcceptInsecureCertificates = true;
                firefoxOptions.AddArguments("-purgecaches", "-private", "--disable-gpu", "--disable-direct-write", "--disable-display-color-calibration", "--allow-http-screen-capture", "--disable-accelerated-2d-canvas");
                Driver = new FirefoxDriver("./", firefoxOptions, TimeSpan.FromSeconds(120));
                Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(120);
                Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            #endregion

            Driver.Navigate().GoToUrl("https://www.swisslife.ch/de/private.html/");
            var parent = Driver.FindElement(By.TagName("body"));
            
            /* Sometimes you need the wrapped driver of the parent */
                IWebDriver wrappedDriver = parent.GetDriver();
                Console.WriteLine($"The current window has the title '{wrappedDriver.Title}'");

            /* Find the element inside the parent element safe without exception */
                var labelToScroll = parent.FindElementFirstOrDefault(By.CssSelector("h3.a-heading--style-italic"), 5);
                Console.WriteLine(labelToScroll.Text);

            /* Test ScrollTo() */
                if (labelToScroll != null)
                {
                    labelToScroll.ScrollTo();
                    Console.WriteLine("Scrolled to this label");
                }
            
            /* Find all elementS inside the parent safe without exception */
                var labels = parent.FindElementsOrDefault(By.TagName("h3"));
                Console.WriteLine($"{labels.Count} labels of h3 found");

            /* Real example, accept the disclaimer, if it exists */
                var disclaimerButton = parent.FindElementFirstOrDefault(By.CssSelector("[class*='cookie-disclaimer']>button"), 1);
                if(disclaimerButton != null)
                {
                    disclaimerButton.Click();
                }

            /* Search a not existing element inside the parent, should by null after 5 seconds */
                var notFound = parent.FindElementFirstOrDefault(By.TagName("h99"), 5);
                Console.WriteLine($"Element h99 is {notFound}");

            #region console handling
                Console.WriteLine(" ");
                Console.WriteLine("Press enter to terminate...");
                Console.ReadLine();
            #endregion

            #region teardown browser
                Driver.Close();
                Driver.Dispose();
                Driver.Quit();
            #endregion
        }
    }
}
