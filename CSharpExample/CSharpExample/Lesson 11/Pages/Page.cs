using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CSharpExample.Lesson_11.Pages
{
	internal class Page
	{
		protected IWebDriver driver;
		protected WebDriverWait wait;

		public Page(IWebDriver driver)
		{
			this.driver = driver;
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
		}
	}
}