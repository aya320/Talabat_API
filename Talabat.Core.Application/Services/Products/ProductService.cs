﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction.Common;
using Talabat.Core.Application.Abstraction.Models.Products;
using Talabat.Core.Application.Abstraction.Services.Products;
using Talabat.Core.Application.Common.Exceptions;
using Talabat.Core.Domain.Contracts.Persistence;
using Talabat.Core.Domain.Entities.Products;
using Talabat.Core.Domain.Specifications;
using Talabat.Core.Domain.Specifications.Products;

namespace Talabat.Core.Application.Services.Products
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
	{

		public async Task<Pagination<ProductToReturnDto>> GetProductsAsync(ProductSpecParams specparams)
		{
			var spec = new ProductWithBrandAndCategorySpecfications(specparams.sort ,specparams.BrandId ,specparams.CategoryId ,specparams.PageSize,specparams.PageIndex,specparams.Search);
			
			var Products =await _unitOfWork.GetRepository<Product,int>().GetAllWithSpecAsync(spec);
			var data = _mapper.Map<IEnumerable<ProductToReturnDto>>(Products);
			var countspec = new FilteredProductsForCountSpec(specparams.BrandId, specparams.CategoryId,specparams.Search);
			var count = await _unitOfWork.GetRepository<Product, int>().GetCountAsync(countspec);
			return new Pagination<ProductToReturnDto>(specparams.PageIndex , specparams.PageSize,count ){ Data = data };
		}

		public async Task<ProductToReturnDto> GetProductAsync(int Id)
		{
			var spec = new ProductWithBrandAndCategorySpecfications(Id);

			var Product = await _unitOfWork.GetRepository<Product, int>().GetWithSpecAsync(spec);
			if (Product is null)
				throw new NotFoundException(nameof(Product), Id);
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
