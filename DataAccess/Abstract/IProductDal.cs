using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
	public interface IProductDal:IEntityRepository<Product>
	{
		Product GetByName(string name);
		Product GetByCode(int productCode);
		Product GetByBrand(string brand);
		List<Product> GetByUnitePrice(double min, double max);
		Product GetProductDetail(int id);
	}
}
