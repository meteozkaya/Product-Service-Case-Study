using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
	public class EfUserDal : EfEntityRepositoryBase<User, DatabaseContext>, IUserDal
	{

		public User GetByEmail(string email)
		{
			using(DatabaseContext dbContext = new DatabaseContext())
			{
				try
				{
					User user = dbContext.Users.FirstOrDefault(x => x.Email == email);
					if (user == null)
					{
						throw new EntityNotFoundException("User not found with username " + email.ToString() + " !");
					}
					return user;
				}
				catch (Exception exception) { throw exception; }
			}
			
		}

		//public User Add(User user)
		//{
		//	using (var context = new DatabaseContext())
		//	{
		//		 Generic ekleme methodunu çağırın
		//		context.Set<User>().Add(user);

		//		 Değişiklikleri kaydet
		//		context.SaveChanges();
		//		return user;
		//	}
		//}
	}
}
