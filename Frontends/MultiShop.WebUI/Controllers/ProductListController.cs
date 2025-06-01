using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CommentDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Controllers
{
	public class ProductListController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;
		public ProductListController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}
		public IActionResult Index(string id)
		{
			ViewBag.directory1 = "Ana Sayfa";
			ViewBag.directory2 = "Ürünler";
			ViewBag.directory3 = "Ürün Listesi";
			ViewBag.i = id;
			return View();
		}
		public IActionResult ProductDetail(string id)
		{
			ViewBag.directory1 = "Ana Sayfa";
			ViewBag.directory2 = "Ürünler";
			ViewBag.directory3 = "Ürün Detayları";
			ViewBag.x = id;
			return View();
		}
		[HttpGet]
		public PartialViewResult AddComment()
		{
			//var client = _httpClientFactory.CreateClient();
			//var responseMessage = await client.GetAsync("https://localhost:7037/api/Comments/CommentListByProductId/" + id);
			//if (responseMessage.IsSuccessStatusCode)
			//{
			//	var jsonData = await responseMessage.Content.ReadAsStringAsync();
			//	var values = JsonConvert.DeserializeObject<List<ResultCommentDto>>(jsonData);
			//	return PartialView(values);
			//}
			return PartialView();
		}

		[HttpPost]
		public async Task<IActionResult> AddComment(CreateCommentDto createCommentDto)
		{
			createCommentDto.ImageUrl="test";
			createCommentDto.Rating = 1;
			createCommentDto.Status = false;
			createCommentDto.ProductId = "682b18966a7d46ddccd64e50";
			createCommentDto.CreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
			var client = _httpClientFactory.CreateClient();
			var jsonContent = JsonConvert.SerializeObject(createCommentDto);
			StringContent stringContent = new StringContent(jsonContent, Encoding.UTF8, "aplication/json");
			var responseMessage =await client.PostAsync("https://localhost:7037/api/Comments", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index", "Default");
			}
			return View();
		}
	}
}
