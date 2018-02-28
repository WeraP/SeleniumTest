using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using OpenQA.Selenium.Firefox;


namespace CSharpExample
{
	[TestFixture]
	public class CheckItem10
	{
		private IWebDriver driver;
		private WebDriverWait wait;

		[SetUp]
		public void Start()
		{
			//driver = new ChromeDriver();
			//driver = new FirefoxDriver();
			driver = new InternetExplorerDriver();
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
		}

		[Test]
		public void CheckStickers()
		{
			driver.Url = "http://localhost:8080/litecart/en/";
			wait.Until(ExpectedConditions.TitleIs("Online Store | My Store"));

			//IWebElement item = driver.FindElement(By.CssSelector(".name"));
			string nameList = driver.FindElement(By.CssSelector("#box-campaigns .name")).GetAttribute("textContent");

			IWebElement regPriceList = driver.FindElement(By.CssSelector("#box-campaigns .regular-price"));
			string regularPriceList = regPriceList.GetAttribute("textContent");
			string regularPriceListColor = regPriceList.GetCssValue("color");
			// Check Is Grey
			ColorIsGrey(regularPriceListColor);
			//Check Is Line-through
			Assert.True(regPriceList.GetCssValue("text-decoration").Contains("line-through"));

			string regPriceLSize = regPriceList.GetCssValue("font-size");
			var rSize = Convert.ToDouble(regPriceLSize.Substring(0, regPriceLSize.Length-2));


			IWebElement campPriceList = driver.FindElement(By.CssSelector("#box-campaigns .campaign-price"));
			string campaignPriceList = campPriceList.GetAttribute("textContent");
			string campaignPriceListColor = campPriceList.GetCssValue("color");
			// Check  is Red
			ColorIsRed(campaignPriceListColor);

			// Check is BOLD
			Assert.GreaterOrEqual(int.Parse(campPriceList.GetCssValue("font-weight")), 700);

			string campPriceLSize = campPriceList.GetCssValue("font-size");
			campPriceLSize = campPriceLSize.Substring(0, campPriceLSize.Length-2);
			var cSize = Convert.ToDouble(campPriceLSize);

			Assert.Less(rSize, cSize);
			

			Debug.WriteLine(nameList + regularPriceList + campaignPriceList + regularPriceListColor + " " + campaignPriceListColor);

			driver.FindElement(By.CssSelector("#box-campaigns .link")).Click();
			wait.Until(ExpectedConditions.ElementExists(By.CssSelector("#box-product")));

		
			string nameItem = driver.FindElement(By.CssSelector("#box-product h1")).GetAttribute("textContent");
			Assert.AreEqual(nameList, nameItem);


			IWebElement regPriceItem = driver.FindElement(By.CssSelector("#box-product .regular-price"));
			string regularPriceItem = regPriceItem.GetAttribute("textContent");
			Assert.AreEqual(regularPriceList,regularPriceItem);
			string regularPriceItemColor = regPriceItem.GetCssValue("color");

			// Check Is Grey
			ColorIsGrey(regularPriceItemColor);
			//Check Is Line-through
			Assert.True(regPriceItem.GetCssValue("text-decoration").Contains("line-through"));

			string regPriceISize = regPriceItem.GetCssValue("font-size");
			var rISize = Convert.ToDouble(regPriceISize.Substring(0, regPriceISize.Length - 2));

			IWebElement campPriceItem = driver.FindElement(By.CssSelector("#box-product .campaign-price"));
			string campaignPriceItem = campPriceItem.GetAttribute("textContent");

			Assert.AreEqual(campaignPriceList,campaignPriceItem);

			string campaignPriceItemColor = campPriceItem.GetCssValue("color");

			// Check  is Red
			ColorIsRed(campaignPriceItemColor);

			// Check is BOLD
			Assert.GreaterOrEqual(int.Parse(campPriceItem.GetCssValue("font-weight")), 700);

			string campPriceISize = campPriceItem.GetCssValue("font-size");
			campPriceISize = campPriceISize.Substring(0, campPriceISize.Length - 2);
			var cISize = Convert.ToDouble(campPriceISize);

			Assert.Less(rISize, cISize);

			Debug.WriteLine(nameItem + regularPriceItem + campaignPriceItem);

		}
		private static void ColorIsGrey(string color)
		{
			color = color.Replace("rgba(", "").Replace("rgb(", "").Replace(" ", "").Replace(")", "");

			string r = color.Split(',')[0];
			string g = color.Split(',')[1];
			string b = color.Split(',')[2];

			Assert.AreEqual(b, g);
			Assert.AreEqual(b, r);
		}

		private static void ColorIsRed(string color)
		{
			color = color.Replace("rgba(", "").Replace("rgb(", "").Replace(" ", "").Replace(")", "");

			string g = color.Split(',')[1];
			string b = color.Split(',')[2];

			Assert.AreEqual(g, "0");
			Assert.AreEqual(b, "0");
		}
		[TearDown]
		public void Stop()
		{
			driver.Quit();
			driver = null;
		}

	}
}
