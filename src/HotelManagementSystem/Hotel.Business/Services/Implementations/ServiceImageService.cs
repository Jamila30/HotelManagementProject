namespace Hotel.Business.Services.Implementations
{
	public class ServiceImageService : IServiceImageService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _env;
		public ServiceImageService(IMapper mapper, IWebHostEnvironment env,IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_env = env;
			_unitOfWork = unitOfWork;
		}

		public async Task<List<ServiceImageDto>> GetAllAsync()
		{
			var listAll = await _unitOfWork.serviceImageRepository.GetAll().ToListAsync();
			var listDto = _mapper.Map<List<ServiceImageDto>>(listAll);
			return listDto;
		}


		public async Task<List<ServiceImageDto>> GetByCondition(Expression<Func<ServiceImage, bool>> expression)
		{
			var listAll = await _unitOfWork.serviceImageRepository.GetAll().Where(expression).ToListAsync();
			var listDto = _mapper.Map<List<ServiceImageDto>>(listAll);
			return listDto;
		}

		public async Task<ServiceImageDto?> GetByIdAsync(int id)
		{

			var serviceImage = await _unitOfWork.serviceImageRepository.GetByIdAsync(id);
			if (serviceImage is null) throw new NotFoundException("Element not found");
			var serviceDto = _mapper.Map<ServiceImageDto>(serviceImage);
			return serviceDto;
		}

		public async Task CreateImageForServiceId(int serviceId, CreateServiceImageDto entity)
		{
			var serviceOffer = await _unitOfWork.serviceOfferRepository.GetAll().Include(x => x.ServiceImages).FirstOrDefaultAsync(x => x.Id == serviceId);
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
				try
				{

					fileName = await entity.Image.CopyFileToAsync(@"C:\Users\Asus\Desktop\", "reactpro", "src", "assets", "images");
				}
				catch (Exception)
				{
					throw new BadRequestException("  file didnt created");
				}

				image = fileName;

			}
			ServiceImage serviceImage = new()
			{
				ServiceOffer= serviceOffer,
				ServiceOfferId = serviceId,
				Image = image,
			};
			if(serviceOffer.ServiceImages !=null)
			{
				serviceOffer.ServiceImages.Add(serviceImage);
			}
			string last;
			string next;
			bool check = false;
			var serviceImages = _unitOfWork.serviceImageRepository.GetAll().Include(x => x.ServiceOffer).ToList();
			foreach (var item in serviceImages)
			{
				if (item.Image != null && serviceImage.Image != null)
				{
					last = item.Image[36..];
					next = serviceImage.Image[36..]; ;
					if (last.Equals(next)) { check = true; }
				}
			
			}
			if (check == true) throw new RepeatedImageException("this image exists ");
			await _unitOfWork.serviceImageRepository.Create(serviceImage);
			await _unitOfWork.SaveAsync();
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
			if (id != entity.Id) throw new IncorrectIdException("Id didnt match each other");
			var imageService = _unitOfWork.serviceImageRepository.GetAll().Include(x => x.ServiceOffer).FirstOrDefault(x => x.Id == id);
			if (imageService is null) throw new NotFoundException("There is no Image for update");
			string fileName = string.Empty;
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

				try
				{

					fileName = await entity.Image.CopyFileToAsync(@"C:\Users\Asus\Desktop\", "reactpro", "src", "assets", "images");
				}
				catch (Exception)
				{
					throw new BadRequestException(" new file didnt created");
				}

				imageService.Image = fileName;
			}

			var offer = _unitOfWork.serviceOfferRepository.GetAll().FirstOrDefault(x => x.Id == entity.ServiceOfferId);
			if (offer is null) throw new BadRequestException("there is no Service for set Image for this ServiceOfferId");
			imageService.ServiceOfferId = entity.ServiceOfferId;

			string last;
			string next;
			bool checkImage = false;
			var serviceImages = _unitOfWork.serviceImageRepository.GetAll().Include(x => x.ServiceOffer).ToList();
			foreach (var item in serviceImages)
			{
				if (item.Image != null && imageService.Image!=null  )
				{
					last = item.Image[36..];
					next = imageService.Image[36..];
					if (last.Equals(next)&& item.Id!=imageService.Id) { checkImage = true; }

				}
			}
			if (checkImage == true) throw new RepeatedImageException("this image exists");
			
			_unitOfWork.serviceImageRepository.Update(imageService);
			await _unitOfWork.SaveAsync();
		}
		public async Task Delete(int id)
		{
			var image = _unitOfWork.serviceImageRepository.GetAll().FirstOrDefault(x => x.Id == id);
			if (image is null) throw new NotFoundException("There is no Image for delete");
			if (image.Image != null)
			{
				Helper.DeleteFile(@"C:\Users\Asus\Desktop\", "reactpro", "src", "assets", "images", image.Image);
			}
			_unitOfWork.serviceImageRepository.Delete(image);
			await _unitOfWork.SaveAsync();
		}

	}
}
