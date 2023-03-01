namespace Hotel.Business.Services.Interfaces
{
	public interface IServiceOfferService
	{
		Task<List<ServiceOfferDto>> GetAllAsync();
		Task<List<ServiceOfferDto>> GetByCondition(Expression<Func<ServiceOffer, bool>> expression);
		Task<ServiceOfferDto?> GetByIdAsync(int id);
		//Task<WholeServiceInfoDto?> GetAllByIdAsync(int id);

		Task Create(CreateServiceOfferDto entity);
	
		Task Update(int id, UpdateServiceOfferDto entity);
		Task Delete(int id);
	}
}
