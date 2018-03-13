using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using System.Threading;
using OpenQA.Selenium.Firefox;
using System.Collections.Generic;
using System.Linq;

namespace CSharpExample
{
	[TestFixture]
	public class EditCountry14 : General
	{
		[Test]
		public void EditCountry()
		{
			LoginAdmin();
			driver.Url = "http://localhost:8080/litecart/admin/?app=countries&doc=countries";
			wait.Until(ExpectedConditions.TitleIs("Countries | My Store"));


			driver.FindElements(By.CssSelector("a[href*=AD]"))[0].Click();

			string mainWindow = driver.CurrentWindowHandle;
			ICollection<string> oldWindows = driver.WindowHandles;

			IWebElement[] elementsLink = driver.FindElements(By.CssSelector("td > a[target=_blank]")).ToArray();
			Assert.AreEqual(7, elementsLink.Length);

			foreach (var elementLink in elementsLink)
			{
				elementLink.Click();
				MoveToWindow(mainWindow, oldWindows);
			}

		}

		private void MoveToWindow(string mainWindow, ICollection<string> oldWindows)
		{
			
			
			var newWindow = wait.Until(ThereIsWindowOtherThan(oldWindows));
			Debug.WriteLine(newWindow);


			driver.SwitchTo().Window(newWindow);
			driver.Close();
			driver.SwitchTo().Window(mainWindow);
		}
	}
}
