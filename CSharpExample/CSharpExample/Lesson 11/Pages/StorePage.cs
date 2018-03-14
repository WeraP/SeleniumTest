using System.Linq;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace CSharpExample.Lesson_11.Pages
{
	
	internal class StorePage : Page
	{
		public StorePage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }
		internal StorePage Open()
		{
			driver.Url = "http://localhost:8080/litecart/en/";
			wait.Until(ExpectedConditions.TitleIs("Online Store | My Store"));
			return this;
		}

		[FindsBy(How = How.CssSelector, Using = ".image-wrapper")]
		internal IWebElement FirstProduct;


	}
}
