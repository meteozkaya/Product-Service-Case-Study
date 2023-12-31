﻿using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
	public interface IUserDal:IEntityRepository<User>
	{
		public User GetByEmail(string email);

		//public User Add(User user);
	}
}
