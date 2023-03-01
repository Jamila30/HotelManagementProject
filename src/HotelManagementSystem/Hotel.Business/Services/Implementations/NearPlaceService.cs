namespace Hotel.Business.Services.Implementations
{
	public class NearPlaceService:INearPlaceService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _env;
		private readonly IMapper _mapper;

		public NearPlaceService(IWebHostEnvironment env, IMapper mapper, IUnitOfWork unitOfWork)
		{
			_env = env;
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}
		public async Task<List<NearPlaceDto>> GetAllAsync()
		{
			var listAll = await _unitOfWork.nearPlaceRepository.GetAll().ToListAsync();
			var listDto = _mapper.Map<List<NearPlaceDto>>(listAll);
			return listDto;

		}

		public async Task<List<NearPlaceDto>> GetByCondition(Expression<Func<NearPlace, bool>> expression)
		{
			var listAll = await _unitOfWork.nearPlaceRepository.GetAll().Where(expression).ToListAsync();
			var listDto = _mapper.Map<List<NearPlaceDto>>(listAll);
			return listDto;
		}

		public async Task<NearPlaceDto?> GetByIdAsync(int id)
		{
			var place = await _unitOfWork.nearPlaceRepository.GetByIdAsync(id);
			if (place is null) throw new NotFoundException("Element not found");
			var placeDto = _mapper.Map<NearPlaceDto>(place);
			return placeDto;
		}
		public async Task Create(CreateNearPlaceDto entity)
		{
			NearPlace place = new()
			{
				Description = entity.Description,
				Title = entity.Title,
			};
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

					throw new BadRequestException(" file didnt created");
				}
				
				place.Image = fileName;

			}
			await _unitOfWork.nearPlaceRepository.Create(place);
			await _unitOfWork.SaveAsync();
		}
		public async Task UpdateAsync(int id, UpdateNearPlaceDto entity)
		{
			if (id != entity.Id) throw new IncorrectIdException("Id doesn't match each other");
			var place = await _unitOfWork.nearPlaceRepository.GetByIdAsync(id);
			if (place is null) throw new NotFoundException("Not Found");
			place.Description = entity.Description;
			place.Title = entity.Title;

			if (entity.Image != null)
			{
				if (!entity.Image.CheckFileSize(100))
				{
					//throw  ExceptionsDictionary.MyExceptions["Enter Suitable File Size"];
					throw new IncorrectFileSizeException("Enter Suitable File Size");
				}
				if (!entity.Image.CheckFileFormat("image/"))
				{
					throw new IncorrectFileSizeException("Enter Suitable File Format");
				}

				string fileName = string.Empty;
				try
				{
					fileName = await entity.Image.CopyFileToAsync(@"C:\Users\Asus\Desktop\", "reactpro", "src", "assets", "images");
				}
				catch (Exception)
				{

					throw new BadRequestException(" file didnt created");
				}
				place.Image = fileName;
			}

			_unitOfWork.nearPlaceRepository.Update(place);
			await _unitOfWork.SaveAsync();

		}

		public async Task Delete(int id)
		{
			var place = await _unitOfWork.nearPlaceRepository.GetByIdAsync(id);
			if (place is null) throw new NotFoundException("Not Found");
			if (place.Image != null)
			{
				Helper.DeleteFile(@"C:\Users\Asus\Desktop\","reactpro", "src", "assets", "images", place.Image);
			}
			_unitOfWork.nearPlaceRepository.Delete(place);
			await _unitOfWork.SaveAsync();
		}
	}
}
