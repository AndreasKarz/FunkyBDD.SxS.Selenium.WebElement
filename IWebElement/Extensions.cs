using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;

namespace FunkyBDD.SxS.Selenium.WebElement
{
    public static class Extensions
    {
        private static IWebDriver webDriver(IWebElement element)
        {
            return ((IWrapsDriver)element).WrappedDriver;
        }

        private static IJavaScriptExecutor javaScriptExecutor(IWebElement element)
        {
            return (IJavaScriptExecutor)webDriver(element);
        }

        /// <summary>
        ///   Get the wrapped IWebDriver of the Component
        /// </summary>
        public static IWebDriver GetDriver(this IWebElement element) => webDriver(element);

        /// <summary>
        ///   Scroll to the element, safer then with Selenium Action
        /// </summary>
        /// <param name="element"></param>
        public static void ScrollTo(this IWebElement element)
        {
            javaScriptExecutor(element).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        /// <summary>
        ///   Get the property of the computed style of the element
        /// </summary>
        /// <param name="element">Self</param>
        /// <param name="property">The property of the pseudo element e.g. 'background-color'</param>
        /// <returns></returns>
        public static string GetComputedStyle(this IWebElement element, string property)
        {
            var result = javaScriptExecutor(element).ExecuteScript($"return window.getComputedStyle(arguments[0], '::before').getPropertyValue('{property}');", element);
            return result.ToString();
        }

        /// <summary>
        ///     Get the element inside this parent element matching the current by criteria
        /// </summary>
        /// <param name="element">Selenium IWebElement to search inside</param>
        /// <param name="by">Selenium By selector</param>
        /// <param name="explicitWait">Timeout in seconds to set explicitWait to find the element, default 5</param>
        /// <returns>First matching IWebElement or Null</returns>
        public static IWebElement FindElementFirstOrDefault(this IWebElement element, By by, int explicitWait = 5)
        {
            var driver = webDriver(element);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(explicitWait));
            IWebElement result;

            try
            {
                result = wait.Until(
                    d => {
                        try
                        {
                            return element.FindElement(by);
                        }
                        catch (Exception)
                        {
                            return null;
                        }
                    }
                );
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }

        /// <summary>
        ///     Get the elements inside this parent element matching the current by criteria
        /// </summary>
        /// <param name="element">Selenium IWebElement to search inside</param>
        /// <param name="by">Selenium By selector</param>
        /// <param name="explicitWait">Timeout in seconds to set explicitWait to find the element, default 5</param>
        /// <returns>ReadOnlyCollection<IWebElement> or Null</returns>
        public static ReadOnlyCollection<IWebElement> FindElementsOrDefault(this IWebElement element, By by, int explicitWait = 5)
        {
            var driver = webDriver(element);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(explicitWait));
            ReadOnlyCollection<IWebElement> result = element.FindElements(by);

            try
            {
                result = wait.Until(
                    d => {
                        try
                        {
                            var tResult = element.FindElements(by);
                            if (tResult.Count == 0)
                            {
                                return null;
                            }
                            else
                            {
                                return tResult;
                            }
                        }
                        catch (Exception)
                        {
                            return null;
                        }
                    }
                );
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }
    }
}
