using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace CSharpExample
{
	[TestFixture]
	public class Registration11
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
		public void Registration()
		{
			driver.Url = "http://localhost:8080/litecart/en/create_account";
			wait.Until(ExpectedConditions.TitleIs("Create Account | My Store"));

			IWebElement taxId = driver.FindElement(By.CssSelector("input[name='tax_id']"));
			taxId.SendKeys("12345");

			IWebElement company = driver.FindElement(By.CssSelector("input[name='company']"));
			company.SendKeys("Company");

			IWebElement firstname = driver.FindElement(By.CssSelector("input[name='firstname']"));
			firstname.SendKeys("firstname" + Keys.Enter);

			IWebElement lastname = driver.FindElement(By.CssSelector("input[name='lastname']"));
			lastname.SendKeys("lastname" + Keys.Enter);

			IWebElement address1 = driver.FindElement(By.CssSelector("input[name='address1']"));
			address1.SendKeys("address1" + Keys.Enter);

			IWebElement postcode = driver.FindElement(By.CssSelector("input[name='postcode']"));
			postcode.SendKeys("12345" + Keys.Enter);

			IWebElement city = driver.FindElement(By.CssSelector("input[name='city']"));
			city.SendKeys("Irvine"+Keys.Enter);

			var countrySelect = new SelectElement(driver.FindElement(By.CssSelector("select[name=country_code]")));
			countrySelect.SelectByText("United States");

			IWebElement email = driver.FindElement(By.CssSelector("input[name='email']"));
			string emailText = "test" + DateTime.Now.Ticks + "@test.com";
			email.SendKeys(emailText + Keys.Enter);

			IWebElement phone = driver.FindElement(By.CssSelector("input[name='phone']"));
			phone.SendKeys("+71234567901" + Keys.Enter);

			IWebElement password = driver.FindElement(By.CssSelector("input[name='password']"));
			password.SendKeys("test" + Keys.Enter);

			IWebElement confirmed_password = driver.FindElement(By.CssSelector("input[name='confirmed_password']"));
			confirmed_password.SendKeys("test" + Keys.Enter);


			//driver.FindElement(By.CssSelector("button[name='create_account']")).Click();
			wait.Until(ExpectedConditions.TitleIs("Online Store | My Store"));

			driver.FindElement(By.CssSelector("#box-account > div > ul > li:nth-child(4) > a")).Click();
			wait.Until(ExpectedConditions.ElementExists(By.CssSelector("input[name='email']")));

			driver.FindElement(By.CssSelector("input[name='email']")).SendKeys(emailText);
			driver.FindElement(By.CssSelector("input[name='password']")).SendKeys("test");
			driver.FindElement(By.CssSelector("button[name='login']")).Click();

			wait.Until(ExpectedConditions.ElementExists(By.CssSelector("#box-account > div > ul > li:nth-child(4) > a")));
			driver.FindElement(By.CssSelector("#box-account > div > ul > li:nth-child(4) > a")).Click();

			

		}
		[TearDown]
		public void Stop()
		{
			driver.Quit();
			driver = null;
		}

	}
}
