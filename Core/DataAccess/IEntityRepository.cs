﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
	public interface IEntityRepository<T> where T : class, IEntity, new()
	{
		T Get(Expression<Func<T, bool>> filter);
		T Add(T entity);
		bool Delete(int id);
		T Update(T entity);
	}
}