using System;
using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Remote;

namespace FunkyBDD.SxS.Selenium.WebElement
{

    /// <summary>
    ///     Default properties for all selenium selector atoms
    /// </summary>
    /// <remarks>
    ///     Author:         andreas.karz@swisslife.ch
    ///     Last Change:    14.08.2019
    /// </remarks>
    public class DefaultProps
    {
        /// <summary>
        ///   IWebElement to hold the base
        /// </summary>
        public IWebElement Component;

        /// <summary>
        ///   Get the wrapped IWebDriver of the Component
        /// </summary>
        public IWebDriver Driver => ((IWrapsDriver)Component).WrappedDriver;

        /// <summary>
        ///   Handle for JavaScriptExecuter
        /// </summary>
        public IJavaScriptExecutor JavaScriptExecutor => ((IJavaScriptExecutor)Driver);

        /// <summary>
        ///   Returns the registered browser name of the wrapped driver from this element
        /// </summary>
        public string BrowserName => ((RemoteWebDriver)Driver).Capabilities["browserName"].ToString();

        /// <summary>
        ///     The with of the label in pixel
        /// </summary>
        public int Width => Component.Size.Width;

        /// <summary>
        ///     The height of the label in pixel
        /// </summary>
        public int Height => Component.Size.Height;

        /// <summary>
        ///     Indicate whether or not the component is displayed
        /// </summary>
        public bool Displayed => Component.Displayed;

        /// <summary>
        ///     Indicate whether or not the component is enabled for operations
        /// </summary>
        public bool Enabled => Component.Enabled;

        /// <summary>
        ///     Indicate whether or not the component was found
        /// </summary>
        public bool Found => Component != null;

        /// <summary>
        ///     Position from the LEFT in Pixel
        /// </summary>
        public int X => _componentLocation(Component).X;

        /// <summary>
        ///     Position from the TOP in Pixel
        /// </summary>
        public int Y => _componentLocation(Component).Y;

        /// <summary>
        ///     Get the text color as CSS value string
        /// </summary>
        public string Color => Component.GetCssValue("color");

        /// <summary>
        ///     Get the background color as CSS value string
        /// </summary>
        public string BackgroundColor => Component.GetCssValue("background-color");

        /// <summary>
        ///     Workaround for Safari Browsers
        /// </summary>
        /// <param name="component"></param>
        /// <returns>Location of the element</returns>
        private static Point _componentLocation(IWebElement component)
        {
            try
            {
                return component.Location;
            }
            catch (Exception)
            {
                return ((RemoteWebElement)component).LocationOnScreenOnceScrolledIntoView;
            }
        }
    }
}
