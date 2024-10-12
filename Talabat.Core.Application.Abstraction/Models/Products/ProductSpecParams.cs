using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Application.Abstraction.Models.Products
{
	public class ProductSpecParams
	{
		public string? sort { get; set; }
		public int? brandId { get; set; }
		public int? categoryId { get; set; }
		public int PageIndex { get; set; } = 1;
		private const int MaxSize = 10;
		private  int PageSize = 5;
		public int pageSize 
		{
			get {  return pageSize; }
			set { pageSize =value >MaxSize ? MaxSize :value ; }
		}
	}
}
