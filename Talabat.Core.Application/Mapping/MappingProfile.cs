using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction.Models.Basket;
using Talabat.Core.Application.Abstraction.Models.Orders;
using Talabat.Core.Application.Abstraction.Models.Products;
using Talabat.Core.Domain.Entities.Basket;
using Talabat.Core.Domain.Entities.Orders;
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

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(dest => dest.DeliveryMethod, options => options.MapFrom(src => src.DeliveryMethod!.ShortName));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductId, options => options.MapFrom(src => src.Product.ProductId))
                .ForMember(dest => dest.ProductName, options => options.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.PictureUrl, options => options.MapFrom<OrderItemPictureUrlResolver>());
            CreateMap<Address, AddressDto>();
            CreateMap<DeliveryMethod, DeliveryMethodDto>();




        }
	}
}
