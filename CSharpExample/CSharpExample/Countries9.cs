using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;

namespace CSharpExample
{
	[TestFixture]
	public class Countries9
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
		public void Countries()
		{
			driver.Url = "http://localhost:8080/litecart/admin/login.php";
			driver.FindElement(By.Name("username")).SendKeys("admin");
			driver.FindElement(By.Name("password")).SendKeys("admin");
			driver.FindElement(By.Name("login")).Click();
			wait.Until(ExpectedConditions.TitleIs("My Store"));

			driver.FindElement(By.CssSelector("#box-apps-menu > li:nth-child(3)")).Click();
			wait.Until(ExpectedConditions.TitleIs("Countries | My Store"));

			var table = driver.FindElement(By.CssSelector(".dataTable"));
			var countries = table.FindElements(By.CssSelector("tr.row > td:nth-child(5)"));

			string prevCountry = "0";
			foreach (IWebElement country in countries)
			{
				string nameCountry = country.GetAttribute("textContent");

				Assert.IsTrue(String.Compare(prevCountry, nameCountry) < 0);
				prevCountry = nameCountry;
				Debug.WriteLine(prevCountry);
			}
			// sort Zone
			int zonesCount = table.FindElements(By.CssSelector("tr.row > td:nth-child(6)")).Count;

			for (int i = 0; i < zonesCount; i++)
			{
				var zones = table.FindElements(By.CssSelector("tr.row > td:nth-child(6)"));
				int qtyZone = Convert.ToInt16(zones[i].GetAttribute("textContent"));
				if (qtyZone > 0)
				{
					driver.FindElements(By.CssSelector("tr.row > td:nth-child(5) > a"))[i].Click();
					wait.Until(ExpectedConditions.ElementExists(By.CssSelector("h2")));

					var zonesName = driver.FindElements(By.CssSelector("td:nth-child(3):not(#content)"));
					int zonesQty = zonesName.Count-1;

					string prevZone = "";

					for (int j = 0; j < zonesQty; j++)
					{
						string zoneName = zonesName[j].Text;
						Assert.IsTrue(String.Compare(prevZone, zoneName) < 0);
						prevZone = zoneName;
						Debug.WriteLine(prevZone);
					}
					driver.Url = "http://localhost:8080/litecart/admin/?app=countries&doc=countries";
					wait.Until(ExpectedConditions.TitleIs("Countries | My Store"));
					table = driver.FindElement(By.CssSelector(".dataTable"));
					
				}


			}
		}
		[TearDown]
		public void Stop()
		{
			driver.Quit(); 
			driver = null;
		}

	}
}
