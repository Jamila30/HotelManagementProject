namespace Hotel.Business.Services.Implementations
{
	public class WhyUsService : IWhyUsService
	{

		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _env;
		public WhyUsService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_env = env;
		}

		public async Task<List<WhyUsDto>> GetAllAsync()
		{
			var listAll = await _unitOfWork.whyUsRepository.GetAll().ToListAsync();
			var listDto = _mapper.Map<List<WhyUsDto>>(listAll);
			return listDto;

		}

		public async Task<List<WhyUsDto>> GetByCondition(Expression<Func<WhyUs, bool>> expression)
		{
			var listAll = await _unitOfWork.whyUsRepository.GetAll().Where(expression).ToListAsync();
			var listDto = _mapper.Map<List<WhyUsDto>>(listAll);
			return listDto;
		}

		public async Task<WhyUsDto?> GetByIdAsync(int id)
		{
			var why = await _unitOfWork.whyUsRepository.GetByIdAsync(id);
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


				try
				{
					why.Image = await entity.Image.CopyFileToAsync(@"C:\Users\Asus\Desktop\", "reactpro", "src", "assets", "images");
				}
				catch (Exception)
				{

					throw new BadRequestException("file didnt created");
				}


			}
			await _unitOfWork.whyUsRepository.Create(why);
			await _unitOfWork.SaveAsync();
		}
		public async Task UpdateAsync(int id, UpdateWhyUsDto entity)
		{
			if (id != entity.Id) throw new IncorrectIdException("Id doesn't match each other");
			var why = await _unitOfWork.whyUsRepository.GetByIdAsync(id);
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


				try
				{
					why.Image = await entity.Image.CopyFileToAsync(@"C:\Users\Asus\Desktop\", "reactpro", "src", "assets", "images");
				}
				catch (Exception)
				{

					throw new BadRequestException(" new file didnt created");
				}
				

			}

			_unitOfWork.whyUsRepository.Update(why);
			await _unitOfWork.SaveAsync();

		}

		public async Task Delete(int id)
		{
			var why = await _unitOfWork.whyUsRepository.GetByIdAsync(id);
			if (why is null) throw new NotFoundException("Not Found");
			if (why.Image != null)
			{
				Helper.DeleteFile(@"C:\Users\Asus\Desktop\", "reactpro", "src", "assets", "images", why.Image);
			}
			_unitOfWork.whyUsRepository.Delete(why);
			await _unitOfWork.SaveAsync();
		}

	}
}
