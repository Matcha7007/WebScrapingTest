﻿using Microsoft.AspNetCore.Http;
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
		[HttpPost]
		public IEnumerable<Scraping> Scrap(string url)
		{
			IWebDriver driver = new ChromeDriver();
			driver.Manage().Window.Maximize();
			driver.Navigate().GoToUrl(url);
			try
			{
				String itemName = driver.FindElement(By.XPath("//h1[@data-testid='lblPDPDetailProductName']")).Text;
				String itemPrice = driver.FindElement(By.XPath("//div[@data-testid='lblPDPDetailProductPrice']")).Text;
				String itemDescription = driver.FindElement(By.XPath("//div[@data-testid='lblPDPDescriptionProduk']")).Text;
				String itemVendor = driver.FindElement(By.XPath("//a[@data-testid='llbPDPFooterShopName']/h2")).Text;

				Console.WriteLine("Name: " + itemName);
				Console.WriteLine(itemPrice);
				Console.WriteLine(itemDescription);
				Console.WriteLine(itemVendor);

				driver.Close();
				return Enumerable.Range(1, 1).Select(index => new Scraping
				{
					ItemName = itemName,
					ItemPrice = itemPrice,
					ItemDescription = itemDescription,
					ItemVendor = itemVendor
				}).ToArray();
			}
			catch (Exception ex)
			{
				driver.Close();
				return (IEnumerable<Scraping>)BadRequest(ex.Message);
			}
		}
	}
}