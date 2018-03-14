using System.Linq;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace CSharpExample.Lesson_11.Pages
{

	internal class BasketPage : Page
	{

		public BasketPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

		internal void Open()
		{
			driver.Url = "http://localhost:8080/litecart/en/checkout";
		}

		[FindsBy(How = How.CssSelector, Using = "button[name=remove_cart_item]")]
		internal IWebElement RemoveCartItemButton;

	}
}
