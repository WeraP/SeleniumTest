using System;
using System.Collections.Generic;
using NUnit.Framework;


namespace CSharpExample.Lesson_11.Pages
{
	[TestFixture]
	public class ProductsCartTests : TestBase 
	{
		[Test]
		public void CartPO()
		{

			app.AddProductsToCart();

			app.CheckoutCart();

			app.DeleteFromCart();
		}
	}
}
