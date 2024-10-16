using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction.Services;
using Talabat.Core.Application.Abstraction.Services.Basket;
using Talabat.Core.Application.Abstraction.Services.Employees;
using Talabat.Core.Application.Abstraction.Services.Products;
using Talabat.Core.Application.Services.Basket;
using Talabat.Core.Application.Services.Employees;
using Talabat.Core.Application.Services.Products;
using Talabat.Core.Domain.Contracts.Persistence;

namespace Talabat.Core.Application.Services
{
    internal class ServiceManager : IServiceManager
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IConfiguration _configuration;
		private readonly Lazy< IProductService > _productService;
		private readonly Lazy< IBasketService > _basketService;

		private readonly Lazy<IEmployeeService> _employeeServices;
		public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper ,IConfiguration configuration ,Func<IBasketService> basketServiceFactory )
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_configuration = configuration;
			_productService = new Lazy<IProductService>(()=>new ProductService(_unitOfWork,_mapper));
			_employeeServices = new Lazy<IEmployeeService>(() => new EmployeeServices(_unitOfWork, _mapper));
			_basketService = new Lazy<IBasketService>(basketServiceFactory);

		}

		public IProductService ProductService  => _productService.Value;
		public IEmployeeService EmployeeServices => _employeeServices.Value;

		public IBasketService BasketService => _basketService.Value;
	}
}
