using Core.Entities;
using Entities.Concrete;
using Entities.Dtos.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface IProductService
	{
		Product Add(Product product);
		bool Delete(int id);
		Product Update(Product product);
		Product GetByName(string name);
		Product GetByCode(int productCode);
		Product GetByBrand(string brand);
		List<Product> GetByUnitePrice(double min, double max);
		Product GetProductDetail(int id);

	}
}
