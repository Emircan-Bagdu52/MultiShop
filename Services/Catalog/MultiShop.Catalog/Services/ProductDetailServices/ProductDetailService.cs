﻿using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductDetailDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Services.ProductDetailServices;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductDetailServices
{
	public class ProductDetailService : IProductDetailService
	{

		private readonly IMapper _mapper;

		public ProductDetailService(IMapper mapper, IDatabaseSettings _databaseSettings)
		{
			var client = new MongoClient(_databaseSettings.ConnectionString);
			var database = client.GetDatabase(_databaseSettings.DatabaseName);
			_ProductDetailCollection = database.GetCollection<ProductDetail>(_databaseSettings.ProductDetailCollectionName);
			_mapper = mapper;
		}

		private readonly IMongoCollection<ProductDetail> _ProductDetailCollection;
		public async Task CreatProductDetailAsync(CreateProductDetailDto createProductDetailDto)
		{
			var values = _mapper.Map<ProductDetail>(createProductDetailDto);
			await _ProductDetailCollection.InsertOneAsync(values);
		}

		public async Task DeleteProductDetailAsync(string id)
		{
			await _ProductDetailCollection.DeleteOneAsync(x => x.ProductDetailID == id);
		}

		public async Task<List<ResultProductDetailDto>> GetAllProductDetailAsync()
		{
			var values = await _ProductDetailCollection.Find(x => true).ToListAsync();
			return _mapper.Map<List<ResultProductDetailDto>>(values);
		}

		public async Task<GetByIdProductDetailDto> GetByIdProductDetailAsync(string id)
		{
			var values = await _ProductDetailCollection.Find<ProductDetail>(x => x.ProductDetailID == id).FirstOrDefaultAsync();
			return _mapper.Map<GetByIdProductDetailDto>(values);
		}

		public async Task UpdateProductDetailAsync(UpdateProductDetailDto updateProductDetailDto)
		{
			var values = _mapper.Map<ProductDetail>(updateProductDetailDto);
			await _ProductDetailCollection.FindOneAndReplaceAsync(X => X.ProductDetailID == updateProductDetailDto.ProductDetailID, values);
		}

		public async Task<GetByIdProductDetailDto> GetByProductIdProductDetailAsync(string id)
		{
			var values = await _ProductDetailCollection.Find<ProductDetail>(x => x.ProductId == id).FirstOrDefaultAsync();
			return _mapper.Map<GetByIdProductDetailDto>(values);
		}
	}
}