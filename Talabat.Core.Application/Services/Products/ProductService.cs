﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction.Models.Products;
using Talabat.Core.Application.Abstraction.Services.Products;
using Talabat.Core.Domain.Contracts;
using Talabat.Core.Domain.Entities.Products;

namespace Talabat.Core.Application.Services.Products
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
	{

		public async Task<IEnumerable<ProductToReturnDto>> GetProductsAsync()
		{
			var Products=await _unitOfWork.GetRepository<Product,int>().GetAllAsync();
			var ProductToReturn = _mapper.Map<IEnumerable<ProductToReturnDto>>(Products);
			return ProductToReturn;
		}

		public async Task<ProductToReturnDto> GetProductAsync(int Id)
		{
			var Product = await _unitOfWork.GetRepository<Product, int>().GetAsync(Id);
			var ProductToReturn=_mapper.Map<ProductToReturnDto>(Product);
			return ProductToReturn;


		}



		public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
		{
			var Brands =await _unitOfWork.GetRepository<ProductBrand,int>().GetAllAsync();
			var BrandsToReturn =_mapper.Map<IEnumerable< BrandDto>>(Brands);
			return BrandsToReturn;
		}

		public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
		{
			var Categories = await _unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync();
			var CategoriesToReturn = _mapper.Map<IEnumerable<CategoryDto>>(Categories);
			return CategoriesToReturn;
		}

		
	}
}