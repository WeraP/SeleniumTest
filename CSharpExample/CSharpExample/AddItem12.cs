using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace CSharpExample
{
	[TestFixture]
	public class AddItem12
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
		public void AddItem()
		{
			LoginAdmin();

			driver.Url = "http://localhost:8080/litecart/admin/?app=catalog&doc=catalog";
			wait.Until(ExpectedConditions.TitleIs("Catalog | My Store"));

			//qty products before

			var itemsBefore = driver.FindElements(By.CssSelector("table.dataTable tr.row")).Count;

			//driver.FindElement(By.CssSelector(".button:nth-child(2)")).Click();
			driver.FindElement(By.CssSelector("a.button[href*=product]")).Click();
			wait.Until(ExpectedConditions.TitleIs("Add New Product | My Store"));


			//GENERAL
			driver.FindElement(By.CssSelector("input[name='name[en]']")).SendKeys("TestName");
			driver.FindElement(By.CssSelector("input[name=code]")).SendKeys("TestCode");

			driver.FindElement(By.CssSelector("input[data-name='Rubber Ducks']")).Click();

			var defaultCategorySelect = new SelectElement(driver.FindElement(By.CssSelector("select[name=default_category_id]")));
			defaultCategorySelect.SelectByText("Rubber Ducks");

			driver.FindElement(By.CssSelector("input[value='1-1']")).Click();

			driver.FindElement(By.CssSelector("input[name='quantity']")).Clear();
			driver.FindElement(By.CssSelector("input[name='quantity']")).SendKeys("20");

			//Load image
			var imageFileInfo = new FileInfo(Path.Combine
				(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),@"images\img.jpg"));
			driver.FindElement(By.CssSelector("input[type=file]")).SendKeys(imageFileInfo.FullName);

			driver.FindElement(By.CssSelector("input[name='date_valid_from']")).SendKeys("02/03/2018");
			driver.FindElement(By.CssSelector("input[name='date_valid_to']")).SendKeys("04/05/2020");

			//INFORMATION

			driver.FindElement(By.CssSelector("a[href*=tab-information]")).Click();
			wait.Until(ExpectedConditions.ElementExists(By.CssSelector("#tab-information")));

			var ManufacturerSelect = new SelectElement(driver.FindElement(By.CssSelector("select[name=manufacturer_id]")));
			defaultCategorySelect.SelectByIndex(1);

			driver.FindElement(By.CssSelector("input[name = 'keywords']")).SendKeys("test, keywords");
			driver.FindElement(By.CssSelector("input[name='short_description[en]")).SendKeys("short description test");
			driver.FindElement(By.CssSelector(".trumbowyg-editor")).SendKeys("description test");
			driver.FindElement(By.CssSelector("input[name = 'head_title[en]']")).SendKeys("head_title_test");
			driver.FindElement(By.CssSelector("input[name = 'meta_description[en]']")).SendKeys("meta description test");


			//PRICES

			driver.FindElement(By.CssSelector("a[href*=tab-prices]")).Click();
			wait.Until(ExpectedConditions.ElementExists(By.CssSelector("#tab-prices")));

			driver.FindElement(By.CssSelector("input[name='purchase_price']")).Clear();
			driver.FindElement(By.CssSelector("input[name='purchase_price']")).SendKeys("20.15");

			var currencyCodeSelect = new SelectElement(driver.FindElement(By.CssSelector("select[name=purchase_price_currency_code]")));
			currencyCodeSelect.SelectByValue("USD");

			driver.FindElement(By.CssSelector("input[name='prices[USD]']")).SendKeys("25");
			driver.FindElement(By.CssSelector("input[name='prices[EUR]']")).SendKeys("23.5");

			//SAVE

			driver.FindElement(By.CssSelector("button[name=save]")).Click();
			wait.Until(ExpectedConditions.TitleIs("Catalog | My Store"));

			//Qty products after

			var itemsAfter = driver.FindElements(By.CssSelector("table.dataTable tr.row")).Count;
			Assert.IsTrue(itemsAfter - itemsBefore == 1);
		}

		private void LoginAdmin()
		{
			driver.Url = "http://localhost:8080/litecart/admin/login.php";
			driver.FindElement(By.Name("username")).SendKeys("admin");
			driver.FindElement(By.Name("password")).SendKeys("admin");
			driver.FindElement(By.Name("login")).Click();
			wait.Until(ExpectedConditions.TitleIs("My Store"));
		}

		[TearDown]
		public void Stop()
		{
			driver.Quit();
			driver = null;
		}
	}
}
