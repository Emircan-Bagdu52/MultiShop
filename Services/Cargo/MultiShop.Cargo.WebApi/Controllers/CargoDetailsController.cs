using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoDetailDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
    [ApiController]
    public class CargoDetailsController : ControllerBase
    {
		private readonly ICargoDetailService _CargoDetailService;

		public CargoDetailsController(ICargoDetailService CargoDetailService)
		{
			_CargoDetailService = CargoDetailService;
		}

		[HttpGet]
		public IActionResult CargoDetailList()
		{
			var result = _CargoDetailService.TGetAll();
			return Ok(result);
		}

		[HttpPost]
		public IActionResult CreateCargoCompnay(CreateCargoDetailDto createCargoDetailDto)
		{
			CargoDetail CargoDetail = new CargoDetail()
			{
				SenderCustomer = createCargoDetailDto.SenderCustomer,
				ReceiverCustomer = createCargoDetailDto.ReceiverCustomer,
				Barcode = createCargoDetailDto.Barcode,
				CargoCompnayId = createCargoDetailDto.CargoCompnayId
			};
			_CargoDetailService.TInsert(CargoDetail);
			return Ok("Kargo detayları başarıyla oluşturuldu.");
		}

		[HttpDelete]
		public IActionResult RemoveCargoDetail(int id)
		{
			_CargoDetailService.TDelete(id);
			return Ok("Kargo detayları başarıyla silindi.");
		}

		[HttpGet("{id}")]
		public IActionResult CargoDetailGetById(int id)
		{
			var result = _CargoDetailService.TGetById(id);
			return Ok(result);
		}

		[HttpPut]
		public IActionResult UpdateCargoDetail(UpdateCargoDetailDto updateCargoDetailDto)
		{
			CargoDetail CargoDetail = new CargoDetail()
			{
				CargoDetailId = updateCargoDetailDto.CargoDetailId,
				SenderCustomer = updateCargoDetailDto.SenderCustomer,
				ReceiverCustomer = updateCargoDetailDto.ReceiverCustomer,
				Barcode = updateCargoDetailDto.Barcode,
				CargoCompnayId = updateCargoDetailDto.CargoCompnayId
			};
			_CargoDetailService.TUpdate(CargoDetail);
			return Ok("Kargo detayları başarıyla güncellendi.");
		}

	}
}