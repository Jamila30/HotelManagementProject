namespace Hotel.Business.Services.Implementations
{
	public class SliderHomeService : ISliderHomeService
	{
		private readonly ISliderHomeRepository _repository;
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _env;
		public SliderHomeService(ISliderHomeRepository repository, IMapper mapper, IWebHostEnvironment env)
		{
			_repository = repository;
			_mapper = mapper;
			_env = env;
		}

		public async Task<List<SliderHomeDto>> GetAllAsync()
		{
			var listAll = await _repository.GetAll().ToListAsync();
			var listDto = _mapper.Map<List<SliderHomeDto>>(listAll);
			return listDto;

		}

		public async Task<List<SliderHomeDto>> GetByCondition(Expression<Func<SliderHome, bool>> expression)
		{
			var listAll = await _repository.GetAll().Where(expression).ToListAsync();
			var listDto = _mapper.Map<List<SliderHomeDto>>(listAll);
			return listDto;
		}

		public async Task<SliderHomeDto?> GetByIdAsync(int id)
		{
			var slider = await _repository.GetByIdAsync(id);
			if (slider is null) throw new NotFoundException("Element not found");
			var sliderDto = _mapper.Map<SliderHomeDto>(slider);
			return sliderDto;
		}
		public async Task Create(CreateSliderHomeDto entity)
		{
			SliderHome slider = new()
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
				slider.Image = entity.Image.CopyFileTo(_env.WebRootPath, "assets", "images", "sliderHome");
			}
			await _repository.Create(slider);
			await _repository.SaveChanges();
		}
		public async Task UpdateAsync(int id, UpdateSliderHomeDto entity)
		{
			if (id != entity.Id) throw new IncorrectIdException("Id doesn't match each other");
			var slider = await _repository.GetByIdAsync(id);
			if (slider is null) throw new NotFoundException("Not Found");
			slider.Description = entity.Description;
			slider.Title = entity.Title;

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
				slider.Image = entity.Image.CopyFileTo(_env.WebRootPath, "assets", "images", "sliderHome");
			}

			_repository.Update(slider);
			await _repository.SaveChanges();

		}

		public async Task Delete(int id)
		{
			var slider = await _repository.GetByIdAsync(id);
			if (slider is null) throw new NotFoundException("Not Found");
			if (slider.Image != null)
			{
				Helper.DeleteFile(_env.WebRootPath, "assets", "images", "sliderHome", slider.Image);
			}
			
			_repository.Delete(slider);
			await _repository.SaveChanges();
		}



	}
}
