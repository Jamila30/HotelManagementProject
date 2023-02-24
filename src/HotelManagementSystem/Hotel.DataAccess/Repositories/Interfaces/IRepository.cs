
namespace Hotel.DataAccess.Repositories.Interfaces
{
	public interface IRepository<T> where T : class,ITableEntity,new()
	{
		IQueryable<T> GetAll();
		IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression);
		Task<T?> GetByIdAsync(int id);
		Task Create(T entity);
		void Delete(T entity);
		void Update(T entity);
		Task SaveChanges();
	}
}
