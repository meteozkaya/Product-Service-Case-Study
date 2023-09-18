using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class ProductManager : IProductService
	{
		private readonly IProductDal _productDal;
		public ProductManager(IProductDal productDal)
		{
			_productDal = productDal;
		}
		public Product Add(Product product)
		{
			return _productDal.Add(product);
		}

		public bool Delete(int id)
		{
			return _productDal.Delete(id);
		}

		public Product GetByBrand(string brand)
		{
			return _productDal.GetByBrand(brand);
		}

		public Product GetByCode(int productCode)
		{
			return _productDal.GetByCode(productCode);
		}

		public Product GetByName(string name)
		{
			return _productDal.GetByName(name);
		}

		public List<Product> GetByUnitePrice(double min, double max)
		{
			return _productDal.GetByUnitePrice(min, max);
		}

		public Product GetProductDetail(int id)
		{
			return _productDal.GetProductDetail(id);
		}

		public Product Update(Product product)
		{
			return _productDal.Update(product);
		}
	}
}
