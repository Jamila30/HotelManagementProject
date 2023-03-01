namespace Hotel.Business.Services.Implementations
{
	public class SliderHomeService : ISliderHomeService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _env;
		public SliderHomeService( IMapper mapper, IWebHostEnvironment env, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_env = env;
			_unitOfWork = unitOfWork;
		}

		public async Task<List<SliderHomeDto>> GetAllAsync()
		{
			var listAll = await _unitOfWork.sliderHomeRepository.GetAll().ToListAsync();
			var listDto = _mapper.Map<List<SliderHomeDto>>(listAll);
			return listDto;

		}

		public async Task<List<SliderHomeDto>> GetByCondition(Expression<Func<SliderHome, bool>> expression)
		{
			var listAll = await _unitOfWork.sliderHomeRepository.GetAll().Where(expression).ToListAsync();
			var listDto = _mapper.Map<List<SliderHomeDto>>(listAll);
			return listDto;
		}

		public async Task<SliderHomeDto?> GetByIdAsync(int id)
		{
			var slider = await _unitOfWork.sliderHomeRepository.GetByIdAsync(id);
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
				try
				{
					slider.Image = await entity.Image.CopyFileToAsync(@"C:\Users\Asus\Desktop\", "reactpro", "src", "assets", "images");
				}
				catch (Exception)
				{

					throw new BadRequestException("new file didnt created");
				}

				
			}
			await _unitOfWork.sliderHomeRepository.Create(slider);
			await _unitOfWork.SaveAsync();
		}
		public async Task UpdateAsync(int id, UpdateSliderHomeDto entity)
		{
			if (id != entity.Id) throw new IncorrectIdException("Id doesn't overlap");
			var slider = await _unitOfWork.sliderHomeRepository.GetByIdAsync(id);
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
			
				try
				{
					slider.Image = await entity.Image.CopyFileToAsync(@"C:\Users\Asus\Desktop\", "reactpro", "src", "assets", "images");
				}
				catch (Exception)
				{

					throw new BadRequestException("new file didnt created");
				}

			}

			_unitOfWork.sliderHomeRepository.Update(slider);
			await _unitOfWork.SaveAsync();

		}

		public async Task Delete(int id)
		{
			var slider = await _unitOfWork.sliderHomeRepository.GetByIdAsync(id);
			if (slider is null) throw new NotFoundException("Not Found");
			if (slider.Image != null)
			{
				Helper.DeleteFile(@"C:\Users\Asus\Desktop\", "reactpro", "src", "assets", "images", slider.Image);
			}
			
			_unitOfWork.sliderHomeRepository.Delete(slider);
			await _unitOfWork.SaveAsync();
		}



	}
}
