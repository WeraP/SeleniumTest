using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace CSharpExample
{
	[TestFixture]
	public class General
	{
		protected IWebDriver driver;
		protected WebDriverWait wait;

		[SetUp]
		public void Start()
		{
			driver = new ChromeDriver();
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
		}

		public void LoginAdmin()
		{
			driver.Url = "http://localhost:8080/litecart/admin/login.php";
			driver.FindElement(By.Name("username")).SendKeys("admin");
			driver.FindElement(By.Name("password")).SendKeys("admin");
			driver.FindElement(By.Name("login")).Click();
			wait.Until(ExpectedConditions.TitleIs("My Store"));
		}

		public static Func<IWebDriver, string> ThereIsWindowOtherThan(IEnumerable<string> oldWindows)
		{
			return driver => driver.WindowHandles.Except(oldWindows).ToList().Single();
		}

		[TearDown]
		public void Stop()
		{
			driver.Quit();
			driver = null;
		}
	}
}
