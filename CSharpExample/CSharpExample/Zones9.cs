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
	public class Zones9 : General
	{
		[Test]
		public void Zones()
		{
			LoginAdmin();
			driver.Url = "http://localhost:8080/litecart/admin/?app=geo_zones&doc=geo_zones";
			wait.Until(ExpectedConditions.TitleIs("Geo Zones | My Store"));

			var table = driver.FindElement(By.CssSelector(".dataTable"));
			var rows = table.FindElements(By.CssSelector("tr.row"));
			int countriesQty = rows.Count;

			for (int i = 0; i < countriesQty; i++ )
			{
				var row = rows[i];
				row.FindElement(By.CssSelector("a")).Click();
				wait.Until(ExpectedConditions.TitleIs("Edit Geo Zone | My Store"));


				var zonesName = driver.FindElements(By.CssSelector("tr:not(.header) > td > select[name*=zone_code ] > option[selected]"));
				int zonesQty = zonesName.Count;

				string prevZone = "";

				for (int j = 1; j < zonesQty; j++)
				{
					string zoneName = zonesName[j].Text;
					Assert.IsTrue(String.Compare(prevZone, zoneName) < 0);
					prevZone = zoneName;
					Debug.WriteLine(prevZone);
				}
				driver.Url = "http://localhost:8080/litecart/admin/?app=geo_zones&doc=geo_zones";
				wait.Until(ExpectedConditions.TitleIs("Geo Zones | My Store"));
				table = driver.FindElement(By.CssSelector(".dataTable"));
				rows = table.FindElements(By.CssSelector("tr.row"));

			}
		}
	}
}
