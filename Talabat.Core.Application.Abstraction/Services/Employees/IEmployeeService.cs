using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction.Models.Employee;

namespace Talabat.Core.Application.Abstraction.Services.Employees
{
	public interface IEmployeeService
	{
		Task<IEnumerable<EmployeeToReturnDto>> GetEmployeeAsync();
		Task<EmployeeToReturnDto> GetEmployeeAsync(int id);
	}
}
