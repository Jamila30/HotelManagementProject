namespace Hotel.Business.Services.Implementations
{
	public class WhyUsService : IWhyUsService
	{

		private readonly IWhyUsRepository _repository;
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _env;
		public WhyUsService(IWhyUsRepository repository, IMapper mapper, IWebHostEnvironment env)
		{
			_repository = repository;
			_mapper = mapper;
			_env = env;
		}

		public async Task<List<WhyUsDto>> GetAllAsync()
		{
			var listAll = await _repository.GetAll().ToListAsync();
			var listDto = _mapper.Map<List<WhyUsDto>>(listAll);
			return listDto;

		}

		public async Task<List<WhyUsDto>> GetByCondition(Expression<Func<WhyUs, bool>> expression)
		{
			var listAll = await _repository.GetAll().Where(expression).ToListAsync();
			var listDto = _mapper.Map<List<WhyUsDto>>(listAll);
			return listDto;
		}

		public async Task<WhyUsDto?> GetByIdAsync(int id)
		{
			var why = await _repository.GetByIdAsync(id);
			if (why is null) throw new NotFoundException("Element not found");
			var whyDto = _mapper.Map<WhyUsDto>(why);
			return whyDto;
		}
		public async Task Create(CreateWhyUsDto entity)
		{
			WhyUs why = new()
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


				why.Image = entity.Image.CopyFileTo(_env.WebRootPath, "assets", "images","whyUs");
				

			}
			await _repository.Create(why);
			await _repository.SaveChanges();
		}
		public async Task UpdateAsync(int id, UpdateWhyUsDto entity)
		{
			if (id != entity.Id) throw new IncorrectIdException("Id doesn't match each other");
			var why = await _repository.GetByIdAsync(id);
			if (why is null) throw new NotFoundException("Not Found");
			why.Description = entity.Description;
			why.Title = entity.Title;

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

				
				why.Image = entity.Image.CopyFileTo(_env.WebRootPath, "assets", "images","whyUs");
				

			}

			_repository.Update(why);
			await _repository.SaveChanges();

		}

		public async Task Delete(int id)
		{
			var why = await _repository.GetByIdAsync(id);
			if (why is null) throw new NotFoundException("Not Found");
			if (why.Image != null)
			{
				Helper.DeleteFile(_env.WebRootPath, "assets", "images", "whyUs", why.Image);
			}
			_repository.Delete(why);
			await _repository.SaveChanges();
		}

	}
}
