using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace CSharpExample
{
	[TestFixture]
	public class LoginESRFFTest
	{
		private IWebDriver esrDriver;
		private WebDriverWait wait;

		[SetUp]
		public void StartESR()
		{
			FirefoxOptions options = new FirefoxOptions();
			options.UseLegacyImplementation = true;
			options.BrowserExecutableLocation = @"C:\Program Files (x86)\ESR\Mozilla Firefox\firefox.exe";
			esrDriver = new FirefoxDriver(options);
			wait = new WebDriverWait(esrDriver, TimeSpan.FromSeconds(10));
		}
		[Test]
		public void LitecartLoginESR()
		{
			esrDriver.Url = "http://localhost:8080/litecart/admin/login.php";
			esrDriver.FindElement(By.Name("username")).SendKeys("admin");
			esrDriver.FindElement(By.Name("password")).SendKeys("admin");
			esrDriver.FindElement(By.Name("login")).Click();
			wait.Until(ExpectedConditions.TitleIs("My Store"));
		}
		[TearDown]
		public void Stop()
		{
			esrDriver.Quit();
			esrDriver = null;
		}
	}
}
