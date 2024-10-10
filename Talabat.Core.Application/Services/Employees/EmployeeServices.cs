using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction.Models.Employee;
using Talabat.Core.Application.Abstraction.Services.Employees;
using Talabat.Core.Domain.Contracts.Persistence;
using Talabat.Core.Domain.Entities.Employee;
using Talabat.Core.Domain.Specifications.Employees;

namespace Talabat.Core.Application.Services.Employees
{
	internal class EmployeeServices(IUnitOfWork unitOfWork, IMapper mapper) : IEmployeeService
	{
		public async Task<IEnumerable<EmployeeToReturnDto>> GetEmployeeAsync()
		{
			var spec = new EmployeeWithDepartmentSepecificationes();
			var emp = await unitOfWork.GetRepository<Employee, int>().GetAllWithSpecAsync(spec);
			var employeetoreturn = mapper.Map<IEnumerable<EmployeeToReturnDto>>(emp);
			return employeetoreturn;

		}
		public async Task<EmployeeToReturnDto> GetEmployeeAsync(int id)
		{
			var spec = new EmployeeWithDepartmentSepecificationes(id);
			var employee = await unitOfWork.GetRepository<Employee, int>().GetWithSpecAsync(spec);
			var employeetoreturn = mapper.Map<EmployeeToReturnDto>(employee);
			return employeetoreturn;
		}
	}
}
