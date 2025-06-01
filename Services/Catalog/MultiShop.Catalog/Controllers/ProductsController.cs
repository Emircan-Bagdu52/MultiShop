using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Services.ProductServices;

namespace MultiShop.Catalog.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductsController(IProductService productService)
		{
			_productService = productService;
		}

		[HttpGet]
		public async Task<IActionResult>ProductList()
		{
			var values=await _productService.GettAllProductAsync();
			return Ok(values);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetProductById(string id)
		{
			var values=await _productService.GetByIdProductAsync(id);
			return Ok(values);
		}
		[HttpPost]
		public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
		{
			await _productService.CreateProductAsync(createProductDto);
			return Ok("Ürün başarıyla eklendi");
		}

		[HttpPut]
		public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
		{
			await _productService.UpdateProductAsync(updateProductDto);
			return Ok("Ürün başarıyla güncellendi.");
		}

		[HttpDelete]
		public async Task<IActionResult>DeleteProduct(string id)
		{
			await _productService.DeleteProductAsync(id);
			return Ok("Kategori Başarıyla silindi.");
		}

		[HttpGet("GetProductsWithCategory")]
		public async Task<IActionResult> GetProductsWithCategory()
		{
			var values = await _productService.GetProductsWithCategoryAsync();
			return Ok(values);
		}
		[HttpGet("ProductListWithCategoryByCatetegoryId/{id}")]
		public async Task<IActionResult> ProductListWithCategoryByCatetegoryId(string id)
		{
			var values = await _productService.GetProductsWithCategoryByCatetegoryIdAsync(id);
			return Ok(values);
		}

	}
}
