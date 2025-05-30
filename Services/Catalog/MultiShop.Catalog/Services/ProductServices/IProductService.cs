﻿using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Dtos.ProductDtos;

namespace MultiShop.Catalog.Services.ProductServices
{
	public interface IProductService
	{
		Task<List<ResultProductDto>> GetAllProductAsync();
		Task CreatProductAsync(CreateProductDto createProductDto);
		Task UpdateProductAsync(UpdateProductDto updateProductDto);
		Task DeleteProductAsync(string id);
		Task<GetByIdProductDto> GetByIdProductAsync(string id);
	}
}
