﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Application.Abstraction.Models.Products
{
	public class ProductSpecParams
	{
		private string? search;
		public string? sort { get; set; }
		public int? BrandId { get; set; }
		public int? CategoryId { get; set; }
		public int PageIndex { get; set; } = 1;
		private const int MaxSize = 10;
		private  int pageSize = 5;
		public int PageSize 
		{
			get {  return pageSize; }
			set { pageSize = value >MaxSize ? MaxSize :value ; }
		}
		public string? Search
		{
			get { return search; }
			set { search = value?.ToUpper(); }
		}
	}
}
