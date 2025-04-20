using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoCompanyDtos;
using MultiShop.Cargo.EntityLayer.Concrete;
using System.Security.Cryptography.Xml;

namespace MultiShop.Cargo.WebApi.Controllers
{
	[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoCompaniesController : ControllerBase
    {
        private readonly ICargoCompanyService _cargoCompanyService;

		public CargoCompaniesController(ICargoCompanyService cargoCompanyService)
		{
			_cargoCompanyService = cargoCompanyService;
		}

		[HttpGet]
		public IActionResult CargoCompanyList()
		{
			var result = _cargoCompanyService.TGetAll();
			return Ok(result);
		}

		[HttpPost]
		public IActionResult CreateCargoCompnay(CreateCargoCompanyDto createCargoCompanyDto)
		{
			CargoCompany cargoCompany = new CargoCompany()
			{
				CargoCompanyName = createCargoCompanyDto.CargoCompanyName
			};
			_cargoCompanyService.TInsert(cargoCompany);
			return Ok("Kargo şirketi başarıyla oluşturuldu.");
		}

		[HttpDelete]
		public IActionResult RemoveCargoCompany(int id)
		{
			_cargoCompanyService.TDelete(id);
			return Ok("Kargo şirketi başarıyla silindi.");
		}

		[HttpGet("{id}")]
		public IActionResult CargoCompanyGetById(int id)
		{
			var result = _cargoCompanyService.TGetById(id);
			return Ok(result);
		}

		[HttpPut]
		public IActionResult UpdateCargoCompany(UpdateCargoCompanyDto updateCargoCompanyDto)
		{
			CargoCompany cargoCompany = new CargoCompany()
			{
				CargoCompanyId = updateCargoCompanyDto.CargoCompanyId,
				CargoCompanyName = updateCargoCompanyDto.CargoCompanyName
			};
			_cargoCompanyService.TUpdate(cargoCompany);
			return Ok("Kargo şirketi başarıyla güncellendi.");
		}

	}
}
