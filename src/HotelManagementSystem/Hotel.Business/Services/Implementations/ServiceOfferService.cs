namespace Hotel.Business.Services.Implementations
{
	public class ServiceOfferService : IServiceOfferService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public ServiceOfferService(IMapper mapper, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		public async Task<List<ServiceOfferDto>> GetAllAsync()
		{
			var listAll = await _unitOfWork.serviceOfferRepository.GetAll().ToListAsync();
			var listDto = _mapper.Map<List<ServiceOfferDto>>(listAll);
			return listDto;
		}

		public async Task<List<ServiceOfferDto>> GetByCondition(Expression<Func<ServiceOffer, bool>> expression)
		{
			var listAll = await _unitOfWork.serviceOfferRepository.GetAll().Where(expression).ToListAsync();
			var listDto = _mapper.Map<List<ServiceOfferDto>>(listAll);
			return listDto;
		}

		public async Task<ServiceOfferDto?> GetByIdAsync(int id)
		{
			var service = await _unitOfWork.serviceOfferRepository.GetByIdAsync(id);
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
			await _unitOfWork.serviceOfferRepository.Create(service);
			await _unitOfWork.SaveAsync();
		}
		public async Task Update(int id, UpdateServiceOfferDto entity)
		{
			if (id != entity.Id) throw new IncorrectIdException("Id didnt match each other ");
			var offer=_unitOfWork.serviceOfferRepository.GetAll().FirstOrDefault(x => x.Id == id);
			if (offer is null) throw new NotFoundException("There is no Service for update");
			offer.Title=entity.Title;
			offer.Description=entity.Description;
			offer.IsFree=entity.IsFree;
			offer.Price=entity.Price;
			
			_unitOfWork.serviceOfferRepository.Update(offer);
			 await _unitOfWork.SaveAsync();

		}

		public async Task Delete(int id)
		{
			var offer=_unitOfWork.serviceOfferRepository.GetAll().FirstOrDefault(x => x.Id == id);
			if (offer is null) throw new NotFoundException("There is no Service for to delete");
			_unitOfWork.serviceOfferRepository.Delete(offer);
			 await _unitOfWork.SaveAsync();
		}


	}
}
