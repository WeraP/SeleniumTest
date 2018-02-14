using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace CSharpExample
{
	[TestFixture]
	public class LoginIETest
	{
		private IWebDriver ieDriver;
		private WebDriverWait wait;

		[SetUp]
		public void StartIE()
		{
			ieDriver = new InternetExplorerDriver();
			wait = new WebDriverWait(ieDriver, TimeSpan.FromSeconds(10));
		}
		[Test]
		public void LitecartLoginIE()
		{
			ieDriver.Url = "http://localhost:8080/litecart/admin/login.php";
			ieDriver.FindElement(By.Name("username")).SendKeys("admin");
			ieDriver.FindElement(By.Name("password")).SendKeys("admin");
			ieDriver.FindElement(By.Name("login")).Click();
			wait.Until(ExpectedConditions.TitleIs("My Store"));
		}
		[TearDown]
		public void Stop()
		{
			ieDriver.Quit();
			ieDriver = null;
		}

	}
}
