﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductDetailDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[AllowAnonymous]
	[Route("Admin/ProductImage")]
	public class ProductImageController : Controller
    {
		private readonly IHttpClientFactory _httpClientFactory;
		public ProductImageController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		[Route("ProductImageDetail/{id}")]
		[HttpGet]
		public async Task<IActionResult> ProductImageDetail(string id)
		{

			ViewBag.v1 = "Ana Sayfa";
			ViewBag.v2 = "Ürünler";
			ViewBag.v3 = "Ürün Görsel Güncelleme Sayfası";
			ViewBag.v0 = "Ürün Görsel İşlemleri";
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync($"https://localhost:7015/api/ProductImages/ProductImagesByProductId/{id}");

			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				Console.WriteLine(jsonData); // Buraya breakpoint koyun veya loglayın
				var values = JsonConvert.DeserializeObject<UpdateProductImageDto>(jsonData);
				return View(values);
			}
			return View();
		}

		[Route("ProductImageDetail/{id}")]
		[HttpPost]
		public async Task<IActionResult> ProductImageDetail(UpdateProductImageDto UpdateProductImageDto)
		{
			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(UpdateProductImageDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PutAsync("https://localhost:7015/api/ProductImages", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("ProductListWithCategory", "Product", new { area = "Admin" });
			}
			return View();
		}
	}
}
