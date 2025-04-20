using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoCompanyDtos;
using MultiShop.Cargo.DtoLayer.Dtos.CargoCustomerDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
    [ApiController]
    public class CargoCustomersController : ControllerBase
    {
        private readonly ICargoCustomerService _cargoCustomerService;

		public CargoCustomersController(ICargoCustomerService cargoCustomerService)
		{
			_cargoCustomerService = cargoCustomerService;
		}

		[HttpGet]
		public IActionResult CargoCustomerList()
		{
			var result = _cargoCustomerService.TGetAll();
			return Ok(result);
		}

		[HttpGet("{id}")]
		public IActionResult CargoCustomerGetById(int id)
		{
			var result = _cargoCustomerService.TGetById(id);
			return Ok(result);
		}

		[HttpPost]
		public IActionResult CargoCustomerAdd(CreateCargoCustomerDto createCargoCustomerDto)
		{
			CargoCustomer cargoCustomer = new CargoCustomer()
			{
				Name = createCargoCustomerDto.Name,
				Surname = createCargoCustomerDto.Surname,
				Email = createCargoCustomerDto.Email,
				Phone = createCargoCustomerDto.Phone,
				District = createCargoCustomerDto.District,
				City = createCargoCustomerDto.City,
				Address = createCargoCustomerDto.Address
			};
			_cargoCustomerService.TInsert(cargoCustomer);
			return Ok("Kargo müşteri ekleme işlemi başarıyla yapıldı. ");
		}

		[HttpPut]
		public IActionResult CargoCustomerUpdate(UpdateCargoCustomerDto updateCargoCustomerDto)
		{
			CargoCustomer cargoCustomer = new CargoCustomer()
			{
				CargoCustomerId = updateCargoCustomerDto.CargoCustomerId,
				Name = updateCargoCustomerDto.Name,
				Surname = updateCargoCustomerDto.Surname,
				Email = updateCargoCustomerDto.Email,
				Phone = updateCargoCustomerDto.Phone,
				District = updateCargoCustomerDto.District,
				City = updateCargoCustomerDto.City,
				Address = updateCargoCustomerDto.Address
			};
			_cargoCustomerService.TUpdate(cargoCustomer);
			return Ok("Kargo müşteri güncelleme işlemi başarıyla yapıdı. ");
		}

		[HttpDelete]
		public IActionResult CargoCustomerDelete(int id)
		{
			_cargoCustomerService.TDelete(id);
			return Ok("Kargo müşteri silme işlemi başarıyla yapıldı. ");
		}
	}
}
