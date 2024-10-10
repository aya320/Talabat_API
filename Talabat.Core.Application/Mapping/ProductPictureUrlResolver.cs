using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction.Models.Products;
using Talabat.Core.Domain.Entities.Products;

namespace Talabat.Core.Application.Mapping
{
	public class ProductPictureUrlResolver(IConfiguration configuration) : IValueResolver<Product, ProductToReturnDto, string?>
	{
		public string? Resolve(Product source, ProductToReturnDto destination, string? destMember, ResolutionContext context)
		{
			if(!string.IsNullOrEmpty(source.PictureUrl))
				return $"{configuration["urls:ApiBaseUrl"]}/{source.PictureUrl}";
			return string.Empty ;
		}
	}
}
