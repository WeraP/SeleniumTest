using System.Linq;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;


namespace CSharpExample.Lesson_11.Pages
{
	internal class ProductPage : Page
	{

		public ProductPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

		internal void Checkout()
		{
			driver.FindElement(By.CssSelector("a.link[href*=checkout]")).Click();
		}

		[FindsBy(How = How.CssSelector, Using = "button[name=add_cart_product]")]
		internal IWebElement AddToCartButton;

		[FindsBy(How = How.CssSelector, Using = "select[name*=options]")]
		internal IWebElement SizeProduct;

		[FindsBy(How = How.CssSelector, Using = "span.quantity[style]")]
		internal IWebElement ProductsQty;

		[FindsBy(How = How.CssSelector, Using = "span.quantity")]
		internal IWebElement ProductsFirstQty;

		internal void SelectFirstSize()
		{
			var sizeSelect = new SelectElement(SizeProduct);
			sizeSelect.SelectByIndex(1);
		}
	}
}
