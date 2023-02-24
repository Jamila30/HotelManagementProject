using Hotel.Business.DTOs.ServiceOfferDTOs;

namespace Hotel.Business.Services.Implementations
{
	public class ServiceOfferService : IServiceOfferService
	{
		private readonly IServiceOfferRepository _repository;
		
		private readonly IMapper _mapper;
		
		public ServiceOfferService(IServiceOfferRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<List<ServiceOfferDto>> GetAllAsync()
		{
			var listAll = await _repository.GetAll().ToListAsync();
			var listDto = _mapper.Map<List<ServiceOfferDto>>(listAll);
			return listDto;
		}

		public async Task<List<ServiceOfferDto>> GetByCondition(Expression<Func<ServiceOffer, bool>> expression)
		{
			var listAll = await _repository.GetAll().Where(expression).ToListAsync();
			var listDto = _mapper.Map<List<ServiceOfferDto>>(listAll);
			return listDto;
		}

		public async Task<ServiceOfferDto?> GetByIdAsync(int id)
		{
			var service = await _repository.GetByIdAsync(id);
			if (service is null) throw new NotFoundException("Element not found");
			var serviceDto = _mapper.Map<ServiceOfferDto>(service);
			return serviceDto;
		}

		//public async Task<WholeServiceInfoDto?> GetAllByIdAsync(int id)
		//{

		//	var serviceOffer = _repository.GetAll().FirstOrDefault(x => x.Id == id);
		//	if (serviceOffer is null) throw new NotFoundException("Element not found");
		//	WholeServiceInfoDto? result = new()
		//	{
		//		Id= id,
		//		Description = serviceOffer.Description,
		//		IsFree = serviceOffer.IsFree,
		//		Price = serviceOffer.Price,
		//		Title = serviceOffer.Title,
				
		//	};
		//	var images = _imageRepo.GetAll().Include(x => x.ServiceOffer).Where(x => x.ServiceOfferId == id).ToList();
		//	if (images != null)
		//	{
		//		foreach (var item in images)
		//		{
		//			if (result.Images != null && item.Image != null)
		//			{
		//				result.Images.Add(item.Image);
		//			}
		//		}
		//	}
		//	return result;
		//}

		public async Task Create(CreateServiceOfferDto entity)
		{
			var service = _mapper.Map<ServiceOffer>(entity);
			await _repository.Create(service);
			await _repository.SaveChanges();
		}
		public async Task Update(int id, UpdateServiceOfferDto entity)
		{
			if (id != entity.Id) throw new IncorrectIdException("Id didnt match each other ");
			var offer=_repository.GetAll().FirstOrDefault(x => x.Id == id);
			if (offer is null) throw new NotFoundException("There is no Service for update");
			offer.Title=entity.Title;
			offer.Description=entity.Description;
			offer.IsFree=entity.IsFree;
			offer.Price=entity.Price;
			
			_repository.Update(offer);
			 await _repository.SaveChanges();

		}

		public async Task Delete(int id)
		{
			var offer=_repository.GetAll().FirstOrDefault(x => x.Id == id);
			if (offer is null) throw new NotFoundException("There is no Service for to delete");
			_repository.Delete(offer);
			 await _repository.SaveChanges();
		}


	}
}
