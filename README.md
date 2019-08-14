# FunkyBDD.SxS.Selenium.WebElement
Extensions for the **Selenium IWebElement**. Integrates properties that are always needed. Supports the POM and APOM principles.

```c#
using FunkyBDD.SxS.Selenium.WebElement;

public class Card : DefaultProps
{
    public Card(IWebElement parent)
    {
        Component = parent.FindElement(By.ClassName("m-card"));
    }
}
```

## The following properties are then available:

- **Component**
  Hold the IWebElement
- **Driver**
  Get the wrapped driver of the IWebElement. So you can build a APOM framework without dependency to a IWebDriver
- **JavaScriptExecuter**
  Get a IJavaScriptExecutor based on the wrapped driver
- **BrowserName**
  Get the browser name via the capabilities of the wrapped driver
- **Width**
- **Height**
- **Displayed**
- **Enabled**
- **Found**
  Indicate whether or not the component was found
- **X** and **Y**
  The coordinates of the element. This is browser safe implemented and work with also with Safari
- **Color**
  Get the color of the component defined by CSS or inline HTML
- **BackgroundColor**
  Get the background color of the component defined by CSS or inline HTML



You will find a learning project with examples on [GitHub](https://github.com/AndreasKarz/AutomatedTestingWorkshop)