using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.Response;
using Entities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
	public class EfProductDal : EfEntityRepositoryBase<Product, DatabaseContext>, IProductDal
	{

		public Product GetByBrand(string brand)
		{
			using (var context = new DatabaseContext())
			{
				try
				{
					Product product = context.Products.FirstOrDefault(x => x.Brand == brand && x.IsActive);
					if (product == null)
					{
						throw new EntityNotFoundException("Product not found with username " + brand.ToString() + " !");
					}
					return product;
				}
				catch (Exception exception) { throw exception; }
			}
		}

		public Product GetByCode(int productCode)
		{
			using(var context = new DatabaseContext()) {
				try
				{
					Product product = context.Products.FirstOrDefault(x => x.Code == productCode && x.IsActive);
					if (product == null)
					{
						throw new EntityNotFoundException("Product not found with username " + productCode.ToString() + " !");
					}
					return product;
				}
				catch (Exception exception) { throw exception; }
			}
			
		}


		public Product GetByName(string name)
		{
			using (var context = new DatabaseContext())
			{
				try
				{
					Product product = context.Products.FirstOrDefault(x => x.Name == name && x.IsActive);
					if (product == null)
					{
						throw new EntityNotFoundException("Product not found with username " + name.ToString() + " !");
					}
					return product;
				}
				catch (Exception exception) { throw exception; }
			}
		}

		public List<Product> GetByUnitePrice(double min, double max)
		{
			using (var context = new DatabaseContext())
			{
				try
				{
					var product = context.Products.Where(x => x.Price <= max && x.Price >= min && x.IsActive);
					if (!product.Any())
					{
						throw new EntityNotFoundException("Products not found with in ranges  !");
					}
					return product.ToList();
				}
				catch (Exception exception) { throw exception; }
			}
		}

		public Product GetProductDetail(int id)
		{
			using (var context = new DatabaseContext())
			{
				try
				{
					Product product = (Product)context.Products.Where(x => x.Id == id &&  x.IsActive);
					if (product == null)
					{
						throw new EntityNotFoundException("Product not found with username " + id.ToString() + " !");
					}
					return product;
				}
				catch (Exception exception) { throw exception; }
			}
		}
	}
}
