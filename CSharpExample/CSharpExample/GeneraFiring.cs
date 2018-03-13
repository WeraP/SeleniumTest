using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.Events;

namespace CSharpExample
{
	[TestFixture]
	public class GeneralFiring
	{
		protected EventFiringWebDriver driver;
		protected WebDriverWait wait;

		[SetUp]
		public void Start()
		{
			ChromeOptions options = new ChromeOptions();
			options.SetLoggingPreference(LogType.Browser, LogLevel.All);
			
			driver = new EventFiringWebDriver(new ChromeDriver());
			//driver.FindingElement += driver_FindingElement;
			//driver.FindElementCompleted += (sender,e) => Console.WriteLine(e.FindMethod + " found");
			//driver.ExceptionThrown += (sender, e) => Console.WriteLine(e.ThrownException);
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
		}

		void driver_FindingElement(object sender, FindElementEventArgs e)
		{
			Console.WriteLine(e.FindMethod);
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
