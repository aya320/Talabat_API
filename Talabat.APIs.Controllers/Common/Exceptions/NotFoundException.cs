﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.APIs.Controllers.Common.Exceptions
{
	public class NotFoundException : ApplicationException
	{
		public NotFoundException() : base("Not Found") { }
	}
}
