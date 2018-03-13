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

namespace CSharpExample.Lesson_15
{
	[TestFixture]
	public class PO_Basket_15
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
		public void PageObjectModel()
		{
			{
				int itemsQty = 0;
				int qtyFirst = 0;

				while (itemsQty < 3)
				{
					OpenStore();
					
					qtyFirst = OpenProductAndCheckBasket(qtyFirst);

					//Add To Cart

					itemsQty = AddToCart(itemsQty, qtyFirst);
				}

				DeleteFromCart();
			}
		}

		private void DeleteFromCart()
		{
			driver.FindElement(By.CssSelector("a.link[href*=checkout]")).Click();
			wait.Until(ExpectedConditions.ElementExists(By.CssSelector("td.item")));

			int ItemsInCart = driver.FindElements(By.CssSelector("td.item")).Count;
			Debug.WriteLine(ItemsInCart);

			while (ItemsInCart > 0)
			{
				driver.FindElement(By.CssSelector("button[name = remove_cart_item]")).Click();
				wait.Until(ExpectedConditions.StalenessOf(driver.FindElements(By.CssSelector("td.item"))[0]));
				ItemsInCart--;
			}
		}

		private int AddToCart(int itemsQty, int qtyFirst)
		{
			if (driver.FindElements(By.CssSelector("select[name*=options]")).Count > 0)
			{
				var size = new SelectElement(driver.FindElement(By.CssSelector("select[name*=options]")));
				size.SelectByIndex(1);
			}
			driver.FindElement(By.CssSelector("button[name=add_cart_product]")).Click();
			wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("span.quantity[style]")));

			//wait.Until(ExpectedConditions.ElementExists(By.CssSelector("span.quantity")));
			int qty = Convert.ToInt32(driver.FindElement(By.CssSelector("span.quantity")).GetAttribute("textContent"));

			Assert.AreEqual(1, qty - qtyFirst);
			itemsQty++;
			return itemsQty;
		}

		private int OpenProductAndCheckBasket(int qtyFirst)
		{
			OpenProduct();

			return CheckBasket(ref qtyFirst);
		}

		private int CheckBasket(ref int qtyFirst)
		{
			IWebElement qtyElement = driver.FindElement(By.CssSelector("span.quantity"));
			qtyFirst = Convert.ToInt32(qtyElement.GetAttribute("textContent"));
			return qtyFirst;
		}

		private void OpenProduct()
		{
			driver.FindElement(By.CssSelector(".image-wrapper")).Click();
			wait.Until(ExpectedConditions.ElementExists(By.CssSelector("#box-product")));
		}

		private void OpenStore()
		{
			driver.Url = "http://localhost:8080/litecart/en/";
			wait.Until(ExpectedConditions.TitleIs("Online Store | My Store"));
		}

		[TearDown]
		public void Stop()
		{
			driver.Quit();
			driver = null;
		}
	}
}
