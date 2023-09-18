using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
	public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
		where TContext : DbContext, new()
		where TEntity : class, IEntity, new()
	{
		public TEntity Get(Expression<Func<TEntity, bool>> filter)
		{
			using (TContext context = new TContext())
			{
				return context.Set<TEntity>().SingleOrDefault(filter);
			}
		}

		public TEntity GetById(int id)
		{
			using (TContext context = new TContext())
			{
				try
				{
					TEntity entity = context.Set<TEntity>().Find(id);
					if (entity == null)
					{
						throw new Exception("Thing not found with id " + id.ToString() + " !");
					}
					return entity;
				}
				catch (Exception exception) { throw exception; }
			}

		}

		public TEntity Add(TEntity entity)
		{
			using (TContext context = new TContext())
			{
				var addedEntity = context.Entry(entity);
				addedEntity.State = EntityState.Added;
				context.SaveChanges();
				return entity;
			}
		}

		public bool Delete(int id)
		{
			using (TContext context = new TContext()) 
			{
				TEntity entity = GetById(id);
				entity.IsActive = false;
				var deletedEntity = context.Entry(entity);
				deletedEntity.State = EntityState.Modified;
				context.SaveChanges();
				return true;
			}
		}

		public TEntity Update(TEntity entity)
		{
			using (TContext context = new TContext()) 
			{
				var updatedEntity = context.Entry(entity);
				updatedEntity.State = EntityState.Modified;
				context.SaveChanges();
				return entity;
			}

		}

	}
}
