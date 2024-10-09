using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction.Services;
using Talabat.Core.Application.Abstraction.Services.Products;
using Talabat.Core.Application.Services.Products;
using Talabat.Core.Domain.Contracts;

namespace Talabat.Core.Application.Services
{
	internal class ServiceManager : IServiceManager
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly Lazy< IProductService > _productService;


		public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper )
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_productService = new Lazy<IProductService>(()=>new ProductService(_unitOfWork,_mapper)); 

		}

		public IProductService ProductService  => _productService.Value; 
	}
}
