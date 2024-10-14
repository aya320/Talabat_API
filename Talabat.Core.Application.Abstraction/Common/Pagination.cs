using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction.Models.Products;

namespace Talabat.Core.Application.Abstraction.Common
{
	public class Pagination<T>
	{
		//public Pagination(int pageIndex, int pageSize, IEnumerable<ProductToReturnDto> productToReturn)
		//{
		//	PageIndex = pageIndex;
		//	PageSize = pageSize;
		//}

		public Pagination(int pageIndex, int pageSize ,int count)
		{
			PageIndex = pageIndex;
			PageSize = pageSize;
			Count = count;
			
		}

		public int PageIndex { get; set; }
		public int PageSize { get; set; }
		public int Count { get; set; }
		public required IEnumerable<T> Data { get; set; }


	}
}
