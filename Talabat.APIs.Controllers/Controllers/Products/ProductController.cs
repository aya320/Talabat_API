using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.APIs.Controllers.Base;
using Talabat.APIs.Controllers.Errors;
using Talabat.Core.Application.Abstraction.Common;
using Talabat.Core.Application.Abstraction.Models.Products;
using Talabat.Core.Application.Abstraction.Services;

namespace Talabat.APIs.Controllers.Controllers.Products
{
	public class ProductController(IServiceManager _serviceManager) : BaseAPIController
	{

		[Authorize/*(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)*/]
		[HttpGet]
		public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams specparams)
		{
			var Products=await _serviceManager.ProductService.GetProductsAsync(specparams);
			return Ok(Products);
		}

		[HttpGet("{Id:int}")]
		public async Task<ActionResult<ProductToReturnDto>> GetProductById(int Id)
		{
			var Product = await _serviceManager.ProductService.GetProductAsync(Id);
			//if (Product is null)
			//	return NotFound(new ApiResponse(404 ,$"Product with id : {Id} is Not Found"));

			return Ok(Product);
		}

		[HttpGet("brands")]
		public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
		{
			var brands = await _serviceManager.ProductService.GetBrandsAsync();
			return Ok(brands);
		}

		[HttpGet("categories")] 
		public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
		{
			var categories = await _serviceManager.ProductService.GetCategoriesAsync();
			return Ok(categories);
		}


	}
}
