using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.IdentityDtos.LoginDtos;
using MultiShop.WebUI.Models;
using MultiShop.WebUI.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MultiShop.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IIdentityService _identityService;
		public LoginController(IHttpClientFactory httpClientFactory, IIdentityService identityService)
		{
			_httpClientFactory = httpClientFactory;
			_identityService = identityService;
		}

		[HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(SignInDto signInDto)
        {
            await _identityService.SignIn(signInDto);
            return RedirectToAction("Index", "User");
		}

        
		////[HttpPost]
		//public async Task<IActionResult> SignIn(SignInDto signInDto)
		//{
  //          signInDto.Username = "emircan";
  //          signInDto.Password = "Emircan.52";
  //          await _identityService.SignIn(signInDto);
  //          return RedirectToAction("Index", "User");
		//}


	}
}






//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Mvc;
//using MultiShop.DtoLayer.IdentityDtos.LoginDtos;
//using MultiShop.WebUI.Models;
//using MultiShop.WebUI.Services.Interfaces;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using System.Text.Json;

//namespace MultiShop.WebUI.Controllers
//{
//	public class LoginController : Controller
//	{
//		private readonly IHttpClientFactory _httpClientFactory;
//		private readonly ILoginService _loginService;

//		public LoginController(IHttpClientFactory httpClientFactory, ILoginService loginService)
//		{
//			_httpClientFactory = httpClientFactory;
//			_loginService = loginService;
//		}

//		[HttpGet]
//		public IActionResult Index()
//		{
//			return View();
//		}

//		[HttpPost]
//		public async Task<IActionResult> Index(CreateLoginDto createLoginDto)
//		{
//			var client = _httpClientFactory.CreateClient();
//			var content = new StringContent(JsonSerializer.Serialize(createLoginDto), Encoding.UTF8, "application/json");
//			var response = await client.PostAsync("http://localhost:5001/api/Logins", content);

//			if (response.IsSuccessStatusCode)
//			{
//				var jsonData = await response.Content.ReadAsStringAsync();
//				var tokenModel = JsonSerializer.Deserialize<JwtResponseModel>(jsonData, new JsonSerializerOptions
//				{
//					PropertyNamingPolicy = JsonNamingPolicy.CamelCase
//				});

//				if (tokenModel != null && !string.IsNullOrEmpty(tokenModel.Token))
//				{
//					// JWT Token'ı frontend'e taşımak üzere TempData ile gönderiyoruz
//					TempData["JwtToken"] = tokenModel.Token;

//					// Gerekirse kullanıcı bilgilerini de çözümleyebilirsiniz
//					var handler = new JwtSecurityTokenHandler();
//					var jwtToken = handler.ReadJwtToken(tokenModel.Token);
//					var userName = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
//					TempData["UserName"] = userName;
//					return RedirectToAction("Index", "Default");
//				}
//			}

//			ViewBag.LoginError = "Kullanıcı adı veya şifre hatalı.";
//			return View();
//		}
//	}
//}
