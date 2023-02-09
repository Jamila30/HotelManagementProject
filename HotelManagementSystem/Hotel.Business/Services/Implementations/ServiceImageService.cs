using Hotel.Business.DTOs.ServiceImageDTOs;

namespace Hotel.Business.Services.Implementations
{
	public class ServiceImageService : IServiceImageService
	{
		private readonly IServiceImageRepository _repository;
		private readonly IServiceOfferRepository _offerRepo;
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _env;
		public ServiceImageService(IServiceImageRepository repository, IMapper mapper, IWebHostEnvironment env, IServiceOfferRepository offerRepo)
		{
			_repository = repository;
			_mapper = mapper;
			_env = env;
			_offerRepo = offerRepo;
		}

		public async Task<List<ServiceImageDto>> GetAllAsync()
		{
			var listAll = _repository.GetAll().ToList();
			var listDto = _mapper.Map<List<ServiceImageDto>>(listAll);
			return listDto;
		}


		public async Task<List<ServiceImageDto>> GetByCondition(Expression<Func<ServiceImage, bool>> expression)
		{
			var listAll = _repository.GetAll().Where(expression).ToList();
			var listDto = _mapper.Map<List<ServiceImageDto>>(listAll);
			return listDto;
		}

		public async Task<ServiceImageDto?> GetByIdAsync(int id)
		{

			var serviceImage = await _repository.GetByIdAsync(id);
			if (serviceImage is null) throw new NotFoundException("Element not found");
			var serviceDto = _mapper.Map<ServiceImageDto>(serviceImage);
			return serviceDto;
		}

		public async Task CreateImageForServiceId(int serviceId, CreateServiceImageDto entity)
		{
			var serviceOffer = await _offerRepo.GetAll().Include(x => x.ServiceImages).FirstOrDefaultAsync(x => x.Id == serviceId);
			if (serviceOffer is null) throw new BadRequestException("There is no Service for adding image to it");
			var image = string.Empty;
			if (entity.Image != null)
			{
				if (!entity.Image.CheckFileSize(100))
				{
					//throw  ExceptionsDictionary.MyExceptions["Enter Suitable File Size"];
					throw new IncorrectFileSizeException("Enter Suitable File Size");
				}
				if (!entity.Image.CheckFileFormat("image/"))
				{
					throw new IncorrectFileFormatException("Enter Suitable File Format");
				}

				string fileName = string.Empty;
				fileName = entity.Image.CopyFileTo(_env.WebRootPath, "assets", "images", "serviceImage");
				image = fileName;

			}
			ServiceImage serviceImage=new ()
			{
			  ServiceOfferId= serviceId,
			  Image = image,
			};
			await _repository.Create(serviceImage);
			await _repository.SaveChanges();
		}
		//public async Task UpdateServiceOfferId(int imageId,int serviceId)
		//{
		//	var imageService = _repository.GetAll().Include(x=>x.ServiceOffer).FirstOrDefault(x => x.Id == imageId);
		//	if (imageService is null) throw new NotFoundException("There is no Image for update");
		//	//
		//	imageService.ServiceOfferId = serviceId;
		//	_repository.Update(imageService);
		//	 await _repository.SaveChanges();
		//}
		public async Task UpdateAsync(int id, UpdateServiceImageDto entity)
		{
			if (id != entity.Id) throw new NotFoundException("Id didnt match each other");
			var imageService = _repository.GetAll().FirstOrDefault(x => x.Id == id);
			if (imageService is null) throw new NotFoundException("There is no Image for update");
			if (entity.Image != null)
			{
				if (!entity.Image.CheckFileSize(100))
				{
					//throw  ExceptionsDictionary.MyExceptions["Enter Suitable File Size"];
					throw new IncorrectFileSizeException("Enter Suitable File Size");
				}
				if (!entity.Image.CheckFileFormat("image/"))
				{
					throw new IncorrectFileFormatException("Enter Suitable File Format");
				}

				string fileName = string.Empty;
				fileName = entity.Image.CopyFileTo(_env.WebRootPath, "assets", "images", "serviceImage");
				imageService.Image = fileName;

			}
			var offer=_offerRepo.GetAll().FirstOrDefault(x => x.Id == entity.ServiceOfferId);
			if (offer is null) throw new BadRequestException("there is no Service for set Image for this ServiceOfferId");
			imageService.ServiceOfferId = entity.ServiceOfferId;

			_repository.Update(imageService);
			await _offerRepo.SaveChanges();
		}
		public async Task Delete(int id)
		{
			var image = _repository.GetAll().FirstOrDefault(x => x.Id == id);
			if (image is null) throw new NotFoundException("There is no Image for delete");
			Helper.DeleteFile(_env.WebRootPath, "assets", "images", "serviceImage",image.Image);
			_repository.Delete(image);
			await _repository.SaveChanges();
		}

	}
}
