using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.APIs.Controllers.Base;
using Talabat.Core.Application.Abstraction.Models.Products;
using Talabat.Core.Application.Abstraction.Services;

namespace Talabat.APIs.Controllers.Controllers.Products
{
	public class ProductController(IServiceManager _serviceManager) : BaseAPIController
	{
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetAllProducts()
		{
			var Products=await _serviceManager.ProductService.GetProductsAsync();
			return Ok(Products);
		}

		[HttpGet("{Id:int}")]
		public async Task<ActionResult<ProductToReturnDto>> GetProductById(int Id)
		{
			var Product = await _serviceManager.ProductService.GetProductAsync(Id);
			if (Product is null)
				return NotFound();

			return Ok(Product);
		}
	}
}
