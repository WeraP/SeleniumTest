using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace CSharpExample
{
	[TestFixture]
	public class LoginNightlyTest
	{
		private IWebDriver NightlyDriver;
		private WebDriverWait wait;

		[SetUp]
		public void StartNightly()
		{
			// new scheme
			FirefoxOptions options = new FirefoxOptions();
			options.UseLegacyImplementation = false;
			options.BrowserExecutableLocation = @"C:\Program Files\Firefox Nightly\firefox.exe";
			NightlyDriver = new FirefoxDriver(options);
			wait = new WebDriverWait(NightlyDriver, TimeSpan.FromSeconds(10));
		}
		[Test]
		public void LitecartLoginNightly()
		{
			NightlyDriver.Url = "http://localhost:8080/litecart/admin/login.php";
			NightlyDriver.FindElement(By.Name("username")).SendKeys("admin");
			NightlyDriver.FindElement(By.Name("password")).SendKeys("admin");
			NightlyDriver.FindElement(By.Name("login")).Click();
			wait.Until(ExpectedConditions.TitleIs("My Store"));
		}
		[TearDown]
		public void Stop()
		{
			NightlyDriver.Quit();
			NightlyDriver = null;
		}
	}
}
