﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Application.Abstraction.Models.Employee
{
	public class EmployeeToReturnDto
	{
		public int Id { get; set; }
		public required string Name { get; set; }
		public int? Age { get; set; }
		public decimal Salary { get; set; }
		public int DepartmentId { get; set; }
		public virtual string? Department { get; set; }
	}
}
