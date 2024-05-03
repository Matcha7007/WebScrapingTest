using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using WebScrapingTest;

namespace TestWebAPi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ScrapingController : ControllerBase
	{
		private readonly IScraping _scraping;
		public ScrapingController(IScraping scraping)
		{ 
			_scraping = scraping;
		}

		[HttpPost]
		public ActionResult Scrap(string url)
		{
			IWebDriver driver = new ChromeDriver();
			try
			{
				ScrapingDto result = _scraping.Scrap(url, driver);
				return Ok(result);
			}
			catch(Exception ex)
			{
				driver.Close();
				return BadRequest(ex.Message);
			}
		}
	}
}
