using IdentityServer4.Hosting.LocalApiAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MultiShop.IdentityServer.Dtos;
using MultiShop.IdentityServer.Models;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace MultiShop.IdentityServer.Controller
{
	[Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]")]
    [ApiController]
    public class RegistersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

		public RegistersController(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}

		[HttpPost]
		public async Task<IActionResult> UserRegister(UserRegisterDtos userRegisterDtos)
		{
			
			var values = new ApplicationUser
			{
				UserName = userRegisterDtos.Username,
				Email = userRegisterDtos.Email,
				Name = userRegisterDtos.Name,
				Surname = userRegisterDtos.Surname
			};
			var result = await _userManager.CreateAsync(values, userRegisterDtos.Password);
			if (result.Succeeded)
			{
				return Ok("Kullanıcı başarıyla eklendi.");
			}
			else
			{
				return Ok("Bir hata oluştu tekrar deneyin");
			}
		}
	}
}
