﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MultiShop.IdentityServer.Dtos;
using MultiShop.IdentityServer.Models;
using MultiShop.IdentityServer.Tools;
using System;
using System.Threading.Tasks;

namespace MultiShop.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;


		public LoginsController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
		}

		//[HttpPost]
		//      public async Task<IActionResult> UserLogin(UserLoginDto userLoginDto)
		//      {
		//          var result = await _signInManager.PasswordSignInAsync(userLoginDto.Username, userLoginDto.Password, false, false);
		//          var user = await _userManager.FindByNameAsync(userLoginDto.Username);
		//	if (result.Succeeded)
		//          {
		//              GetCheckAppUserViewModel model = new GetCheckAppUserViewModel();
		//		model.Username = userLoginDto.Username;
		//              model.Id = user.Id;
		//		var token=JwtTokenGenerator.GenerateToken(model);
		//              return Ok(token);
		//	}
		//          else
		//          {
		//              return Ok("Kullanıcı Adı veya Şifre Hatalı");
		//          }
		//      }
		[HttpPost]
		public async Task<IActionResult> UserLogin(UserLoginDto userLoginDto)
		{
			var result = await _signInManager.PasswordSignInAsync(userLoginDto.Username, userLoginDto.Password, false, false);

			var user = await _userManager.FindByNameAsync(userLoginDto.Username);
			if (user == null)
			{
				return BadRequest("Kullanıcı bulunamadı.");
			}

			if (result.Succeeded)
			{
				// Buraya log bırakın
				Console.WriteLine($"Giriş Başarılı - Kullanıcı ID: {user.Id}, Username: {user.UserName}");

				GetCheckAppUserViewModel model = new GetCheckAppUserViewModel
				{
					Username = user.UserName,
					Id = user.Id
				};

				var token = JwtTokenGenerator.GenerateToken(model);
				return Ok(token);
			}
			else
			{
				return BadRequest("Kullanıcı adı veya şifre hatalı.");
			}
		}

	}
}
