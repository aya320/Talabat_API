using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction.Models.Basket;
using Talabat.Core.Application.Abstraction.Models.Products;
using Talabat.Core.Domain.Entities.Basket;
using Talabat.Core.Domain.Entities.Products;

namespace Talabat.Core.Application.Mapping
{
	public class MappingProfile :Profile
	{
        public MappingProfile()
        {
            CreateMap<Product,ProductToReturnDto>()
                .ForMember(b=>b.Brand,o=>o.MapFrom(src=>src.Brand!.Name))
                .ForMember(c=>c.Category,o=>o.MapFrom(src=>src.Category!.Name))
                .ForMember(c=>c.PictureUrl,o=>o.MapFrom<ProductPictureUrlResolver>());
			

			CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductCategory, CategoryDto>();

            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
			CreateMap<BasketItem, BasketItemDto>().ReverseMap();





		}
	}
}
