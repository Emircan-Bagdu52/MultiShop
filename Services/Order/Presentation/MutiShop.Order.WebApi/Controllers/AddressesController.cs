using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers;
using MultiShop.Order.Application.Features.CQRS.Queries.AddressQueries;

namespace MutiShop.Order.WebApi.Controllers
{
	[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly GetAddressQueryHandler _getAddressQueryHandler;
		private readonly CreateAddressCommandHandler _createAddressCommandHandler;
		private readonly UpdateAddressCommandHandler _updateAddressCommandHandler;
		private readonly RemoveAddressCommandHandler     _deleteAddressCommandHandler;
		private readonly GetAddressByIdQueryHandler _getAddressByIdQueryHandler;

		public AddressesController(GetAddressQueryHandler getAddressQueryHandler, CreateAddressCommandHandler createAddressCommandHandler, UpdateAddressCommandHandler updateAddressCommandHandler, RemoveAddressCommandHandler deleteAddressCommandHandler, GetAddressByIdQueryHandler getAddressByIdQueryHandler)
		{
			_getAddressQueryHandler = getAddressQueryHandler;
			_createAddressCommandHandler = createAddressCommandHandler;
			_updateAddressCommandHandler = updateAddressCommandHandler;
			_deleteAddressCommandHandler = deleteAddressCommandHandler;
			_getAddressByIdQueryHandler = getAddressByIdQueryHandler;
		}

		[HttpGet]
		public async Task<IActionResult> AddressList()
		{
			var result = await _getAddressQueryHandler.Handle();
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetAddressById(int id)
		{
			var result = await _getAddressByIdQueryHandler.Handle(new GetAddressByIdQuery(id));
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> CreateAddress(CreateAddressCommand createAddressCommand)
		{
			await _createAddressCommandHandler.Handle(createAddressCommand);
			return Ok("Adres bilgisi başarıyla eklendi.");
		}

		[HttpPut]
		public async Task<IActionResult> UpdateAddress(UpdateAddressCommand updateAddressCommand)
		{
			await _updateAddressCommandHandler.Handle(updateAddressCommand);
			return Ok("Güncelleme işlemi başarıyla tamamlandı");
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteAddress(int id)
		{
			await _deleteAddressCommandHandler.Handle(new RemoveAddressCommand(id));
			return Ok("Silme işlemi başarıyla gerçekleştirildi.");
		}
	}
}
