namespace Hotel.Business.Services.Implementations
{
	public class FlatService : IFlatService
	{
		public Task Create(CreateNearPlaceDto entity)
		{
			throw new NotImplementedException();
		}

		public Task Delete(int id)
		{
			throw new NotImplementedException();
		}

		public Task<List<NearPlaceDto>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<List<NearPlaceDto>> GetByCondition(Expression<Func<NearPlace, bool>> expression)
		{
			throw new NotImplementedException();
		}

		public Task<NearPlaceDto?> GetByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(int id, UpdateNearPlaceDto entity)
		{
			throw new NotImplementedException();
		}
	}
}
