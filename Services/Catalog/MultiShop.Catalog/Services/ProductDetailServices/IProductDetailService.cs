﻿using MultiShop.Catalog.Dtos.ProductDetailDtos;

namespace MultiShop.Catalog.Services.ProductDetailServices
{
	public interface IProductDetailService
	{
		Task<List<ResultProductDetailDto>> GetAllProductDetailAsync();
		Task CreatProductDetailAsync(CreateProductDetailDto createProductDetailDto);
		Task UpdateProductDetailAsync(UpdateProductDetailDto updateProductDetailDto);
		Task DeleteProductDetailAsync(string id);
		Task<GetByIdProductDetailDto> GetByIdProductDetailAsync(string id);
		Task<GetByIdProductDetailDto> GetByProductIdProductDetailAsync(string id);
	}
}
