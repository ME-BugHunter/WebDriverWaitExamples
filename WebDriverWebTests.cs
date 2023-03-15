using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace WebDriverWaitExamples
{
    public class WebDriverWebTests
    {
        private WebDriver driver;
        private const string baseUrl = "http://uitestpractice.com/";
        private WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(baseUrl);
        }

        [TearDown]
        public void closeBrowser()
        {
            driver.Quit();
        }

        [Test]
        public void Test_Wait_ThreadSleep()
        {
            var ajaxLink = driver.FindElement(By.LinkText("AjaxCall"));
            ajaxLink.Click();
            var internalAjaxLink = driver.FindElement(By.LinkText("This is a Ajax link"));
            internalAjaxLink.Click();
            Thread.Sleep(15000);
            var textElement = driver.FindElement(By.ClassName("ContactUs")).Text;

            Assert.That(textElement.Contains("Selenium is a portable software testing"));

            driver.Navigate().Refresh();

            ajaxLink = driver.FindElement(By.LinkText("AjaxCall"));
            ajaxLink.Click();
            internalAjaxLink = driver.FindElement(By.LinkText("This is a Ajax link"));
            internalAjaxLink.Click();
            Thread.Sleep(15000);
            textElement = driver.FindElement(By.ClassName("ContactUs")).Text;

            Assert.That(textElement.Contains("Selenium is a portable software testing"));
        }

        [Test]
        public void Test_Wait_ImplicitWaitExample()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            var ajaxLink = driver.FindElement(By.LinkText("AjaxCall"));
            ajaxLink.Click();
            var internalAjaxLink = driver.FindElement(By.LinkText("This is a Ajax link"));
            internalAjaxLink.Click();
            var textElement = driver.FindElement(By.ClassName("ContactUs")).Text;

            Assert.That(textElement.Contains("Selenium is a portable software testing"));

            driver.Navigate().Refresh();

            ajaxLink = driver.FindElement(By.LinkText("AjaxCall"));
            ajaxLink.Click();
            internalAjaxLink = driver.FindElement(By.LinkText("This is a Ajax link"));
            internalAjaxLink.Click();
            textElement = driver.FindElement(By.ClassName("ContactUs")).Text;

            Assert.That(textElement.Contains("Selenium is a portable software testing"));
        }

        [Test]
        public void Test_Wait_ExplicitWaitExample()
        {
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            var ajaxLink = driver.FindElement(By.LinkText("AjaxCall"));
            ajaxLink.Click();
            var internalAjaxLink = driver.FindElement(By.LinkText("This is a Ajax link"));
            internalAjaxLink.Click();

            var textElement = wait.Until(d =>
            {
                return d.FindElement(By.ClassName("ContactUs")).Text;
            });

            Assert.That(textElement.Contains("Selenium is a portable software testing"));

            driver.Navigate().Refresh();

            ajaxLink = driver.FindElement(By.LinkText("AjaxCall"));
            ajaxLink.Click();
            internalAjaxLink = driver.FindElement(By.LinkText("This is a Ajax link"));
            internalAjaxLink.Click();
            textElement = wait.Until(d =>
            {
                return d.FindElement(By.ClassName("ContactUs")).Text;
            });

            Assert.That(textElement.Contains("Selenium is a portable software testing"));
        }

        [Test]
        public void Test_Wait_ExplicitWait_ExpectedConditions()
        {
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            var ajaxLink = driver.FindElement(By.LinkText("AjaxCall"));
            ajaxLink.Click();
            var internalAjaxLink = driver.FindElement(By.LinkText("This is a Ajax link"));
            internalAjaxLink.Click();

            var textElement = wait.Until(
                ExpectedConditions.ElementIsVisible(By.ClassName("ContactUs")));


            Assert.That(textElement.Text.Contains("Selenium is a portable software testing"));

            driver.Navigate().Refresh();

            ajaxLink = driver.FindElement(By.LinkText("AjaxCall"));
            ajaxLink.Click();
            internalAjaxLink = driver.FindElement(By.LinkText("This is a Ajax link"));
            internalAjaxLink.Click();
            textElement = wait.Until(
                ExpectedConditions.ElementIsVisible(By.ClassName("ContactUs")));

            Assert.That(textElement.Text.Contains("Selenium is a portable software testing"));
        }

        [Test]
        public void Test_Wait_MultipleWaits()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            var ajaxLink = driver.FindElement(By.LinkText("AjaxCall"));
            ajaxLink.Click();
            var internalAjaxLink = driver.FindElement(By.LinkText("This is a Ajax link"));
            internalAjaxLink.Click();

            var image = driver.GetScreenshot();
            image.SaveAsFile("screenshot.png", ScreenshotImageFormat.Png);

            var textElement = wait.Until(d =>
            {
                return d.FindElement(By.ClassName("ContactUs")).Text;
            });

            Assert.That(textElement.Contains("Selenium is a portable software testing"));

        }
    }
}