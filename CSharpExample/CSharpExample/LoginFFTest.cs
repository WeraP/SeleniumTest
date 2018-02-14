using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace CSharpExample
{
	[TestFixture]
	public class LoginFFTest
	{
		private IWebDriver firefoxDriver;
		private WebDriverWait wait;

		[SetUp]
		public void StartFF()
		{
			firefoxDriver = new FirefoxDriver();
			wait = new WebDriverWait(firefoxDriver, TimeSpan.FromSeconds(10));
		}
		[Test]
		public void LitecartLoginFF()
		{
			firefoxDriver.Url = "http://localhost:8080/litecart/admin/login.php";
			firefoxDriver.FindElement(By.Name("username")).SendKeys("admin");
			firefoxDriver.FindElement(By.Name("password")).SendKeys("admin");
			firefoxDriver.FindElement(By.Name("login")).Click();
			wait.Until(ExpectedConditions.TitleIs("My Store"));
		}
		[TearDown]
		public void Stop()
		{
			firefoxDriver.Quit();
			firefoxDriver = null;
		}

	}
}

