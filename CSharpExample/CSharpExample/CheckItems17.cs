using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.Events;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using OpenQA.Selenium.Firefox;

namespace CSharpExample
{
	[TestFixture]
	public class CheckItems17 : GeneralFiring
	{
		[Test]
		public void CheckItems()
		{
			LoginAdmin();

			var logTypes = driver.Manage().Logs.AvailableLogTypes;

			driver.Url = "http://localhost:8080/litecart/admin/?app=catalog&doc=catalog&category_id=1";
			wait.Until(ExpectedConditions.TitleIs("Catalog | My Store"));
			
			int itemsQty = driver.FindElements(By.CssSelector("td:nth-child(3) > a[href*=product_id]")).Count;

			for (var i = 0; i < itemsQty; i++)
			{
				driver.FindElements(By.CssSelector("td:nth-child(3) > a[href*=product_id]"))[i].Click();

				foreach (var logType in logTypes)
				{
					var qtyLog = driver.Manage().Logs.GetLog(logType).Count;
					Console.WriteLine(qtyLog + " Qty;" + logType + " Type");

					foreach (LogEntry l in driver.Manage().Logs.GetLog(logType))
					{
						Console.WriteLine(l);
					}
				}
				driver.Url = "http://localhost:8080/litecart/admin/?app=catalog&doc=catalog&category_id=1";
			}
			
		}
	}
}

