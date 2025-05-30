﻿using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductImageDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductImageServices
{
	public class ProductImageService:IProductImageService
	{ 
	  private readonly IMongoCollection<ProductImage> _ProductImageCollection;
	private readonly IMapper _mapper;

	//MongoDB ile veri tabanı işlemleri.
	public ProductImageService(IMapper mapper, IDatabaseSettings _databaseSettings)
	{
		var client = new MongoClient(_databaseSettings.ConnectionString);//Bağlantı
		var database = client.GetDatabase(_databaseSettings.DatabaseName);//Veri Tabanı
		_ProductImageCollection = database.GetCollection<ProductImage>(_databaseSettings.ProductImageCollectionName);//Tablo
		_mapper = mapper;
	}

	public async Task CreatProductImageAsync(CreateProductImageDto createProductImageDto)
	{
		var value = _mapper.Map<ProductImage>(createProductImageDto);
		await _ProductImageCollection.InsertOneAsync(value);
	}

	public async Task DeleteProductImageAsync(string id)
	{
		await _ProductImageCollection.DeleteOneAsync(x => x.ProductImageID == id);
	}

	public async Task<List<ResultProductImageDto>> GetAllProductImageAsync()
	{
		var values = await _ProductImageCollection.Find(x => true).ToListAsync();
		return _mapper.Map<List<ResultProductImageDto>>(values);
	}

	public async Task<GetByIdProductImageDto> GetByIdProductImageAsync(string id)
	{
		var values = await _ProductImageCollection.Find<ProductImage>(x => x.ProductImageID == id).FirstOrDefaultAsync();
		return _mapper.Map<GetByIdProductImageDto>(values);
	}

	public async Task UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto)
	{
		var values = _mapper.Map<ProductImage>(updateProductImageDto);
		await _ProductImageCollection.FindOneAndReplaceAsync(x => x.ProductImageID == updateProductImageDto.ProductImageID, values);
	}
}
}
