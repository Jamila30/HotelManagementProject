
namespace Hotel.DataAccess.Repositories.Implementations
{
	public class Repository<T> : IRepository<T> where T : class,ITableEntity, new()
	{
		private readonly AppDbContext _context;
		private readonly DbSet<T> _table;
		public Repository(AppDbContext context)
		{
			_context = context;
			_table =_context.Set<T>();
		}

		public IQueryable<T> GetAll()
		{
			 return _table.AsQueryable();
		}

		public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression)
		{
			return _table.AsQueryable().Where(expression);
		}

		public async Task<T?> GetByIdAsync(int id)
		{
		    return await _table.FindAsync(id);
		}
		public async Task Create(T entity)
		{
			_table.Add(entity);
			await _context.SaveChangesAsync();
		}
		public void Update(T entity)
		{
			_table.Update(entity);
		   _context.SaveChanges();
		}
		public void Delete(T entity)
		{
			_table.Remove(entity);
			_context.SaveChanges();	
		}
		public async Task SaveChanges()
		{
			 await  _context.SaveChangesAsync();
		}

	}

}
