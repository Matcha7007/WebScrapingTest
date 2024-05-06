using OpenQA.Selenium;
using System.Drawing;

using TestWebAPi;

namespace WebScrapingTest
{
	public interface IScraping
	{
		public ScrapingDto Scrap(string url, IWebDriver driver);
	}
	public class Scraping : IScraping
	{
		public ScrapingDto Scrap(string url, IWebDriver driver)
		{
			driver.Manage().Window.Size = new System.Drawing.Size(450, 600);
			driver.Manage().Window.Position = new Point(-450, -600);

			driver.Navigate().GoToUrl(url);
			Thread.Sleep(4000);
			IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
			js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");

			String itemName = driver.FindElement(By.XPath("//h1[@data-testid='lblPDPDetailProductName']")).Text;
			String itemPrice = driver.FindElement(By.XPath("//div[@data-testid='lblPDPDetailProductPrice']")).Text;
			String itemDescription = driver.FindElement(By.XPath("//div[@data-testid='lblPDPDescriptionProduk']")).Text;
			String itemVendor = driver.FindElement(By.XPath("//a[@data-testid='llbPDPFooterShopName']/h2")).Text;

			driver.Close();

			return new ScrapingDto
			{
				ItemName = itemName,
				ItemPrice = Convert.ToInt32(itemPrice.Replace("Rp", "").Replace(".", "")),
				ItemDescription = itemDescription,
				ItemVendor = itemVendor
			};
		}
	}
}
