using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction.Services;
using Talabat.Core.Application.Abstraction.Services.Employees;
using Talabat.Core.Application.Abstraction.Services.Products;
using Talabat.Core.Application.Services.Employees;
using Talabat.Core.Application.Services.Products;
using Talabat.Core.Domain.Contracts.Persistence;

namespace Talabat.Core.Application.Services
{
    internal class ServiceManager : IServiceManager
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly Lazy< IProductService > _productService;

		private readonly Lazy<IEmployeeService> _employeeServices;
		public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper )
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_productService = new Lazy<IProductService>(()=>new ProductService(_unitOfWork,_mapper));
			_employeeServices = new Lazy<IEmployeeService>(() => new EmployeeServices(_unitOfWork, _mapper));

		}

		public IProductService ProductService  => _productService.Value;
		public IEmployeeService EmployeeServices => _employeeServices.Value;
	}
}
