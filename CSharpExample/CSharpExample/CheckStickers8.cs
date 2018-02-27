using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace CSharpExample
{
	[TestFixture]
	public class CheckStickers8
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
		public void CheckStickers()
		{
			driver.Url = "http://localhost:8080/litecart/en/";
			wait.Until(ExpectedConditions.TitleIs("Online Store | My Store"));

			var products = driver.FindElements(By.CssSelector(".image-wrapper"));
			foreach (var product in products)
			{
				Assert.IsTrue(product.FindElements(By.CssSelector(".sticker")).Count == 1);

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