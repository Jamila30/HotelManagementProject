using Hotel.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DataAccess.Repositories.Interfaces
{
	public interface IRepository<T> where T : class,IEntity,new()
	{
		IQueryable<T> GetAll();
		IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression);
		Task<T?> GetByIdAsync(int id);
		Task Create(T entity);
		void Delete(T entity);
		void Upate(T entity);
		Task SaveChanges();
	}
}
