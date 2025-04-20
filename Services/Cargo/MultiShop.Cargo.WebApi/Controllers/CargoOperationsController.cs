using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoOperationDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
    [ApiController]
    public class CargoOperationsController : ControllerBase
    {
		private readonly ICargoOperationService _CargoOperationService;

		public CargoOperationsController(ICargoOperationService CargoOperationService)
		{
			_CargoOperationService = CargoOperationService;
		}

		[HttpGet]
		public IActionResult CargoOperationList()
		{
			var result = _CargoOperationService.TGetAll();
			return Ok(result);
		}

		[HttpGet("{id}")]
		public IActionResult CargoOperationGetById(int id)
		{
			var result = _CargoOperationService.TGetById(id);
			return Ok(result);
		}

		[HttpPost]
		public IActionResult CargoOperationAdd(CreateCargoOperationDto createCargoOperationDto)
		{
			CargoOperation CargoOperation = new CargoOperation()
			{
				Barcode = createCargoOperationDto.Barcode,
				Description = createCargoOperationDto.Description,
				OperationDate = createCargoOperationDto.OperationDate
			};
			_CargoOperationService.TInsert(CargoOperation);
			return Ok("Kargo müşteri ekleme işlemi başarıyla yapıldı. ");
		}

		[HttpPut]
		public IActionResult CargoOperationUpdate(UpdateCargoOperationDto updateCargoOperationDto)
		{
			CargoOperation CargoOperation = new CargoOperation()
			{
				Barcode = updateCargoOperationDto.Barcode,
				Description = updateCargoOperationDto.Description,
				OperationDate = updateCargoOperationDto.OperationDate,
				CargoOperationId = updateCargoOperationDto.CargoOperationId
			};
			_CargoOperationService.TUpdate(CargoOperation);
			return Ok("Kargo müşteri güncelleme işlemi başarıyla yapıdı. ");
		}

		[HttpDelete]
		public IActionResult CargoOperationDelete(int id)
		{
			_CargoOperationService.TDelete(id);
			return Ok("Kargo müşteri silme işlemi başarıyla yapıldı. ");
		}
	}
}
