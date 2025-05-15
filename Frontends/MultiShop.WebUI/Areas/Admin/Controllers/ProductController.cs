using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[AllowAnonymous]
	[Route("Admin/Product")]
	public class ProductController : Controller
    {
		private readonly IHttpClientFactory _httpClientFactory;
		public ProductController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		[Route("Index")]
		public async Task<IActionResult> Index()
		{
			ViewBag.v1 = "Ana Sayfa";
			ViewBag.v2 = "Ürünler";
			ViewBag.v3 = "Ürün Listesi";
			ViewBag.v0 = "Ürün İşlemleri";

			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7015/api/Products");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
				return View(values);
			}
			return View();
		}

		[Route("CreateProduct")]
		[HttpGet]
		public async Task<IActionResult> CreateProduct()
		{
			ViewBag.v1 = "Ana Sayfa";
			ViewBag.v2 = "Ürünler";
			ViewBag.v3 = "Yeni Ürün Girişi";
			ViewBag.v0 = "Ürün İşlemleri";
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7015/api/Categories");
			var jsonData = await responseMessage.Content.ReadAsStringAsync();
			var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
			List<SelectListItem> categories = (from x in values
											   select new SelectListItem
											   {
												   Text = x.CategoryName,
												   Value = x.CategoryID
											   }).ToList();
			ViewBag.CategoryValues = categories;
			return View();
		}
		[HttpPost]
		[Route("CreateProduct")]
		public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
		{
			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(createProductDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PostAsync("https://localhost:7015/api/Products", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index","Product", new {area="Admin"});
			}
			return View();
		}

		[Route("UpdateProduct/{id}")]
		[HttpGet]
		public async Task<IActionResult> UpdateProduct(string id)
		{
			ViewBag.v1 = "Ana Sayfa";
			ViewBag.v2 = "Ürünler";
			ViewBag.v3 = "Ürün Güncelleme Sayfası";
			ViewBag.v0 = "Ürün İşlemleri";

			var client = _httpClientFactory.CreateClient();

			// 1. Kategori verilerini getir
			var responseCategory = await client.GetAsync("https://localhost:7015/api/Categories");
			var jsonCategory = await responseCategory.Content.ReadAsStringAsync();
			var categoryValues = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonCategory);
			ViewBag.CategoryValues = categoryValues.Select(x => new SelectListItem
			{
				Text = x.CategoryName,
				Value = x.CategoryID
			}).ToList();

			// 2. Ürün verisini getir
			var responseProduct = await client.GetAsync("https://localhost:7015/api/Products/" + id);
			if (responseProduct.IsSuccessStatusCode)
			{
				var jsonProduct = await responseProduct.Content.ReadAsStringAsync();
				var productDto = JsonConvert.DeserializeObject<UpdateProductDto>(jsonProduct);
				return View(productDto);
			}

			return View(); // hata durumunda boş view döner
		}


		[HttpPost]
		[Route("UpdateProduct/{id}")]
		public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
		{
			ViewBag.v1 = "Ana Sayfa";
			ViewBag.v2 = "Ürünler";
			ViewBag.v3 = "Yeni Ürün Girişi";
			ViewBag.v0 = "Ürün İşlemleri";
			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(updateProductDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PutAsync("https://localhost:7015/api/Products/", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index", "Product", new { area = "Admin" });
			}
			return View();
		}

		[Route("DeleteProduct/{id}")]
		[HttpGet]
		public async Task<IActionResult> DeleteProduct(string id)
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.DeleteAsync("https://localhost:7015/api/Products?id=" + id);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index", "Product", new { area = "Admin" });
			}
			return View();
		}
	}
}
