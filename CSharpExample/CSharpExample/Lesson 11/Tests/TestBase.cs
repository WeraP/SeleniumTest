using NUnit.Framework;

namespace CSharpExample.Lesson_11.Pages
{
	public class TestBase
	{
		public Application app;

		[SetUp]
		public void start()
		{
			app = new Application();
		}

		[TearDown]
		public void stop()
		{
			app.Quit();
			app = null;
		}
	}
}
