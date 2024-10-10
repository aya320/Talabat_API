using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Entities.Employee;

namespace Talabat.Core.Domain.Specifications.Employees
{
	public class EmployeeWithDepartmentSepecificationes : BaseSpecifications<Employee, int>
	{
		public EmployeeWithDepartmentSepecificationes() : base()
		{
			Includes.Add(E => E.Department!);
		}
		public EmployeeWithDepartmentSepecificationes(int id) : base(id)
		{
			Includes.Add(E => E.Department!);
		}
	}
}
