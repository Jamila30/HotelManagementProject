

namespace Hotel.Business.Services.Implementations
{
	public class NearPlaceService:INearPlaceService
	{
		private readonly INearPlaceRepository _repository;
		private readonly IWebHostEnvironment _env;
		private readonly IMapper _mapper;

		public NearPlaceService(INearPlaceRepository repository, IWebHostEnvironment env, IMapper mapper)
		{
			_repository = repository;
			_env = env;
			_mapper = mapper;
		}
		public async Task<List<NearPlaceDto>> GetAllAsync()
		{
			var listAll = await _repository.GetAll().ToListAsync();
			var listDto = _mapper.Map<List<NearPlaceDto>>(listAll);
			return listDto;

		}

		public async Task<List<NearPlaceDto>> GetByCondition(Expression<Func<NearPlace, bool>> expression)
		{
			var listAll = await _repository.GetAll().Where(expression).ToListAsync();
			var listDto = _mapper.Map<List<NearPlaceDto>>(listAll);
			return listDto;
		}

		public async Task<NearPlaceDto?> GetByIdAsync(int id)
		{
			var place = await _repository.GetByIdAsync(id);
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
				fileName = entity.Image.CopyFileTo(_env.WebRootPath, "assets", "images", "nearPlace");
				place.Image = fileName;

			}
			await _repository.Create(place);
			await _repository.SaveChanges();
		}
		public async Task UpdateAsync(int id, UpdateNearPlaceDto entity)
		{
			if (id != entity.Id) throw new IncorrectIdException("Id doesn't match each other");
			var place = await _repository.GetByIdAsync(id);
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
				fileName = entity.Image.CopyFileTo(_env.WebRootPath, "assets", "images", "nearPlace");
				place.Image = fileName;

			}

			_repository.Update(place);
			await _repository.SaveChanges();

		}

		public async Task Delete(int id)
		{
			var place = await _repository.GetByIdAsync(id);
			if (place is null) throw new NotFoundException("Not Found");
			if (place.Image != null)
			{
				Helper.DeleteFile(_env.WebRootPath, "assets", "images", "nearPlace", place.Image);
			}
			_repository.Delete(place);
			await _repository.SaveChanges();
		}
	}
}
