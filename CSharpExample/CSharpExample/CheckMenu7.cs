using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace CSharpExample
{
	[TestFixture]
	public class CheckMenu7
	{
		private IWebDriver driver;
		private WebDriverWait wait;

		[SetUp]
		public void Start()
		{
			driver = new ChromeDriver();
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
		}

		[Test]
		public void CheckMenu()
		{
			driver.Url = "http://localhost:8080/litecart/admin/login.php";
			driver.FindElement(By.Name("username")).SendKeys("admin");
			driver.FindElement(By.Name("password")).SendKeys("admin");
			driver.FindElement(By.Name("login")).Click();
			wait.Until(ExpectedConditions.TitleIs("My Store"));

			var countTopMenuItems = driver.FindElements(By.CssSelector("#box-apps-menu > li")).Count;

			for (int i = 0; i < countTopMenuItems; i++)
			{
				driver.FindElements(By.CssSelector("#box-apps-menu > li"))[i].Click();
				wait.Until(ExpectedConditions.ElementExists(By.CssSelector("#content h1")));

				var countChildMenuItems = driver.FindElements(By.CssSelector("#box-apps-menu > li.selected li")).Count;

				for (int j = 0; j < countChildMenuItems; j++)
				{
					driver.FindElements(By.CssSelector("#box-apps-menu > li.selected li"))[j].Click();
					wait.Until(ExpectedConditions.ElementExists(By.CssSelector("#content h1")));
				}

			}
		}
		[TearDown]
		public void Stop()
		{
			driver.Quit();
			driver = null;
		}

	}
}
