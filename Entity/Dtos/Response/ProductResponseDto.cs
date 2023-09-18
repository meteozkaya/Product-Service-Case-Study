using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Response
{
	public class ProductResponseDto
	{
		public int Code { get; set; }
		public double Price { get; set; }
		public int Stock { get; set; }
		public string Name { get; set; }
		public string Brand { get; set; }
		public string Currency { get; set; }
		public string Detail { get; set; }
	}
}
