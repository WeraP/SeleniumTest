using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

namespace CSharpExample.Lesson_11.Pages
{

	public class Application
	{
		private IWebDriver driver;
		private WebDriverWait wait;

		private StorePage storePage;
		private ProductPage productPage;
		private BasketPage basketPage;

		public Application()
		{
			driver = new ChromeDriver();
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
			storePage = new StorePage(driver);
			productPage = new ProductPage(driver);
			basketPage = new BasketPage(driver);
		}
		public void Quit()
		{
			driver.Quit();
		}

		internal void AddProductsToCart()
		{
			int itemsQty = 0;
			int qtyFirst = 0;


			while (itemsQty < 3)
			{
				storePage.Open();
				storePage.FirstProduct.Click();
				wait.Until(ExpectedConditions.ElementExists(By.CssSelector("#box-product")));



				qtyFirst = Int32.Parse(productPage.ProductsFirstQty.GetAttribute("textContent"));
				if (driver.FindElements(By.CssSelector("select[name*=options]")).Count > 0)
				{
					productPage.SelectFirstSize();
				}

				productPage.AddToCartButton.Click();
				wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("span.quantity[style]")));

				itemsQty = GetQtyProductInCartOnProductPage();
				Assert.AreEqual(1, itemsQty - qtyFirst);
				itemsQty++;

			}

		}

		private int GetQtyProductInCartOnProductPage()
		{
			return Convert.ToInt32(driver.FindElement(By.CssSelector("span.quantity")).GetAttribute("textContent"));
		}

		internal void CheckoutCart()
		{
			productPage.Checkout();
		}

		internal void DeleteFromCart()
		{
			//driver.FindElement(By.CssSelector("a.link[href*=checkout]")).Click();
			//wait.Until(ExpectedConditions.ElementExists(By.CssSelector("td.item")));

			int ItemsInCart = QtyProductsOnCartPage();


			while (ItemsInCart > 0)
			{
				basketPage.RemoveCartItemButton.Click();
				wait.Until(ExpectedConditions.StalenessOf(driver.FindElements(By.CssSelector("td.item"))[0]));
				//ItemsInCart--;
				ItemsInCart = QtyProductsOnCartPage();
			}
		}

		private int QtyProductsOnCartPage()
		{
			return driver.FindElements(By.CssSelector("td.item")).Count;
		}
	}
}
