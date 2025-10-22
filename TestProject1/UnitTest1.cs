using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;


namespace TestProject1
{
    public class Tests
    {

        [Test]
        public void Test1()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.matchingengine.com");
            driver.Manage().Window.Maximize();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            Actions actions = new Actions(driver);

            // Hover over 'Modules' in the header
            IWebElement modulesMenu = wait.Until(d => d.FindElement(By.XPath("//a[text()='Modules']")));
            actions.MoveToElement(modulesMenu).Perform();

            // Click 'Repertoire Management Module'
            IWebElement repertoireModule = wait.Until(d => d.FindElement(By.XPath("//a[text()='Repertoire Management Module']")));
            repertoireModule.Click();

            // Scroll to 'Additional Features' section
            IWebElement additionalFeatures = wait.Until(d => d.FindElement(By.XPath("//h2[text()='Additional Features']")));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", additionalFeatures);

            // Click 'Products Supported'
            IWebElement productsSupported = wait.Until(d => d.FindElement(By.XPath("//html/body/div/div/div/section/div[6]/div/div/div/div[2]/div/div[1]/ul/li[2]/a/span")));
            productsSupported.Click();

            // Assert heading text
            IWebElement heading = wait.Until(d => d.FindElement(By.CssSelector("div.vc_custom_heading")));
            string expectedHeading = "There are several types of Product Supported:";
            Assert.That(heading.Text.Trim(), Is.EqualTo(expectedHeading), "Heading text does not match expected value.");

            // Verify product list
            var expectedProducts = new List<string> 
            {
            "Cue Sheet / AV Work",
            "Recording",
            "Bundle",
            "Advertisement"
            };

            // Adjust XPath to match list items below the heading
            var productItems = driver.FindElements(By.XPath("//div[contains(@class,'wpb_text_column')]//ul/li"));
            var actualProducts = productItems.Select(p => p.Text.Trim()).ToList();

            Assert.IsTrue(expectedProducts.SequenceEqual(actualProducts), "The product lists do not match.");

            Console.WriteLine("All expected products are present.");
            driver.Quit();
        }
    }
}