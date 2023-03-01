namespace Hotel.Business.Services.Implementations
{
	public class AmentityService : IAmentityService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _env;
		public AmentityService(IMapper mapper, IWebHostEnvironment env, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_env = env;
			_unitOfWork = unitOfWork;
		}

		public async Task<List<AmentityDto>> GetAllAsync()
		{
			var list = await _unitOfWork.amentityRepository.GetAll().ToListAsync();
			var listDto = _mapper.Map<List<AmentityDto>>(list);
			return listDto;
		}

		public async Task<List<AmentityDto>> GetByCondition(Expression<Func<Amentity, bool>> expression)
		{
			var list = await _unitOfWork.amentityRepository.GetAll().Where(expression).ToListAsync();
			var listDto = _mapper.Map<List<AmentityDto>>(list);
			return listDto;
		}
		public async Task<List<AmentityDto>> GetAllAmentitiesByFlatId(int flatId)
		{
			List<Amentity> MyAmentities=new List<Amentity>();
			var amentities = await _unitOfWork.amentityRepository.GetAll().Include(x => x.Flats).ToListAsync();
			if (amentities is null) throw new NotFoundException("Any amentity didnt created");
			amentities.ForEach(amentity =>
			{
				if (amentity.Flats != null)
				{
					if (amentity.Flats.Any(x => x.FlatId == flatId))
					{
						MyAmentities.Add(amentity);
					}
				}
			});

			var flatDto = _mapper.Map<List<AmentityDto>>(MyAmentities);
			return flatDto;
		}
		public async Task<AmentityDto?> GetByIdAsync(int id)
		{
			var amentity = await _unitOfWork.amentityRepository.GetByIdAsync(id);
			var amentityDto = _mapper.Map<AmentityDto>(amentity);
			return amentityDto;
		}
		public async Task Create(CreateAmentityDto entity)
		{
			Amentity amentity = new()
			{
				Title = entity.Title,
				Description = entity.Description
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
					amentity.Image = await entity.Image.CopyFileToAsync(@"C:\Users\Asus\Desktop\", "reactpro","src","assets","images");
				}
				catch (Exception)
				{

					throw new BadRequestException("file didnt created");
				}
			}
			var listAmentity = _unitOfWork.amentityRepository.GetAll();
			if (listAmentity != null)
			{
				foreach (var item in listAmentity)
				{
					if (item.Image != null && amentity.Image != null)
					{

						if (item.Image[36..].Equals(amentity.Image[36..]) && (item.Title == amentity.Title))
						{
							throw new RepeatedChoiceException("this amentity already exist with  same image and title ,choose different ones");
						}
						if (item.Title == amentity.Title)
						{
							throw new RepeatedChoiceException(" amentity already exist with this title ");
						}
					}
				}
			}
			await _unitOfWork.amentityRepository.Create(amentity);
			await _unitOfWork.SaveAsync();
		}

		public async Task UpdateAsync(int id, UpdateAmentityDto entity)
		{
			if (id != entity.Id) throw new IncorrectIdException("Id did match another");
			var amentity = await _unitOfWork.amentityRepository.GetByIdAsync(id);
			if (amentity is null) throw new NotFoundException("there is no amentity to update");
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
				//  C:\Users\Asus\Desktop\reactpro\src\assets\images
				try
				{
					amentity.Image = await entity.Image.CopyFileToAsync(@"C:\Users\Asus\Desktop\", "reactpro", "src", "assets", "images");
				}
				catch (Exception)
				{

					throw new BadRequestException(" new file didnt created");
				}

			}
			amentity.Title = entity.Title;
			amentity.Description = entity.Description;
			var listAmentity = _unitOfWork.amentityRepository.GetAll();
			if (listAmentity != null)
			{
				foreach (var item in listAmentity)
				{
					if (item.Image != null && amentity.Image != null)
					{
						if (item.Image[36..].Equals(amentity.Image[36..]) && (item.Title == amentity.Title) && item.Id != amentity.Id)
						{
							throw new RepeatedChoiceException("this amentity has same image and title ,choose different ones");
						}
						if (item.Title == amentity.Title && item.Id != amentity.Id)
						{
							throw new RepeatedChoiceException(" amentity already exist with this title");
						}
					}
				}
			}

			_unitOfWork.amentityRepository.Update(amentity);
			await _unitOfWork.SaveAsync();
		}
		public async Task Delete(int id)
		{
			var amentity = await _unitOfWork.amentityRepository.GetByIdAsync(id);
			if (amentity is null) throw new NotFoundException("there is no amentity to delete");
			if (amentity.Image != null)
			{
				Helper.DeleteFile(@"C:\Users\Asus\Desktop\", "reactpro", "src", "assets", "images", amentity.Image);
			}
			_unitOfWork.amentityRepository.Delete(amentity);
			await _unitOfWork.SaveAsync();
		}

	}
}
