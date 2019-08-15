# FunkyBDD.SxS.Selenium.WebElement
Extensions for the **Selenium IWebElement** with missed methods and properties.

```c#
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;

namespace FunkyBDD.SxS.Selenium.WebElement.Test
{
    class Program
    {
        public static IWebDriver Driver;

        static void Main(string[] args)
        {
            Driver = new FirefoxDriver("./");
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(120);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Driver.Navigate().GoToUrl("https://www.swisslife.ch/de/private.html/");
            var parent = Driver.FindElement(By.TagName("body"));
            
            /* Sometimes you need the wrapped driver of the parent */
            IWebDriver wrappedDriver = parent.GetDriver();
            Console.WriteLine($"The currenttitle '{wrappedDriver.Title}'");

            /* Find the element inside the parent element safe without exception */
            var labelToScroll = parent.FindElementFirstOrDefault(By.CssSelector("h3.a-heading--style-italic"), 5);
            Console.WriteLine(labelToScroll.Text);

            /* Test ScrollTo() */
            if (labelToScroll != null) {
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

            /* Search a not existing element inside the parent, should by null */
            var notFound = parent.FindElementFirstOrDefault(By.TagName("h99"), 5);
            Console.WriteLine($"Element h99 is {notFound}");

            Console.WriteLine(" ");
            Console.WriteLine("Press enter to terminate...");
            Console.ReadLine();

            Driver.Close();
            Driver.Dispose();
            Driver.Quit();
        }
    }
}

```



You will find a learning project with examples based on this package on [GitHub](https://github.com/AndreasKarz/AutomatedTestingWorkshop)