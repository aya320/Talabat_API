using AutoMapper;
using Talabat.Core.Domain.Entities.Products;
using Talabat.Dashboard.Models;

namespace Talabat.Dashboard.Helper
{
    public class MapsProfile : Profile
    {
        public MapsProfile()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
        }
    }
}
