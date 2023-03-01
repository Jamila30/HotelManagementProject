namespace Hotel.Business.Services.Implementations
{
	public class GallaryImageService : IGallaryImageService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _env;
		public GallaryImageService(IMapper mapper, IWebHostEnvironment env,IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_env = env;
			_unitOfWork = unitOfWork;
		}

		public async Task<List<GallaryImageDto>> GetAllAsync()
		{
			var listAll = await _unitOfWork.gallaryImageRepository.GetAll().ToListAsync();
			var listDto = _mapper.Map<List<GallaryImageDto>>(listAll);
			return listDto;
		}

		public async Task<List<GallaryImageDto>> GetByCondition(Expression<Func<GallaryImage, bool>> expression)
		{
			var listAll = await _unitOfWork.gallaryImageRepository.GetAll().Include(x => x.GallaryCatagory).Where(expression).ToListAsync();
			var listDto = _mapper.Map<List<GallaryImageDto>>(listAll);
			return listDto;
		}

		public async Task<GallaryImageDto?> GetByIdAsync(int id)
		{
			var gallary = await _unitOfWork.gallaryImageRepository.GetByIdAsync(id);
			if (gallary is null) throw new NotFoundException("Element not found");
			if (gallary.GallaryCatagory != null && gallary.GallaryCatagory.Images != null)
				foreach (var item in gallary.GallaryCatagory.Images)
				{
					Console.WriteLine(item.Image);
				}
			var gallaryDto = _mapper.Map<GallaryImageDto>(gallary);
			return gallaryDto;
		}
		public async Task Create(CreateGallaryImageDto entity)
		{

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


			}
			var catagory = await _unitOfWork.gallaryCatagoryRepository.GetByIdAsync(entity.GallaryCatagoryId);
			if (catagory is null) throw new BadRequestException("there is no catagory to set for this id");
			GallaryImage gallary = new()
			{
				GallaryCatagory = catagory,
				GallaryCatagoryId = catagory.Id,
				Image = fileName
			};

			if (catagory.Images != null)
			{
				catagory.Images.Add(gallary);
			}
			var images = _unitOfWork.gallaryImageRepository.GetAll().ToList();
			bool check = false;
			bool checkCatagory = false;
			foreach (var item in images)
			{
				if (item.Image != null)
				{
					if (item.Image[36..].Equals(fileName[36..])) check = true;
				}
				if (item.GallaryCatagoryId == entity.GallaryCatagoryId)
				{
					checkCatagory = true;
				}
				if (check != checkCatagory) { check = false; checkCatagory = false; }
			}
			if (check == true && checkCatagory == true) throw new RepeatedImageException("this image exist in this gallary ");


			await _unitOfWork.gallaryImageRepository.Create(gallary);
			await _unitOfWork.SaveAsync();
		}
		public async Task UpdateAsync(int id, UpdateGallaryImageDto entity)
		{
			if (id != entity.Id) throw new IncorrectIdException("Id didnt match each other");
			var gallary =await	_unitOfWork.gallaryImageRepository.GetAll().Include(x => x.GallaryCatagory).FirstOrDefaultAsync(x => x.Id == id);
			if (gallary is null) throw new NotFoundException("There is no Gallary for update");
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

					
				gallary.Image = fileName;
			}
			var images = _unitOfWork.gallaryImageRepository.GetAll().ToList();
			bool check = false;
			bool checkCatagory = false;
			var last = string.Empty;
			var next = string.Empty;
			foreach (var item in images)
			{
				if (item.Image != null && gallary.Image != null)
				{
					last = item.Image[36..];
					next = gallary.Image[36..];
					if (last == next && item.Id != gallary.Id) check = true;
				}
				if (item.GallaryCatagoryId == entity.GallaryCatagoryId)
				{
					checkCatagory = true;
				}
				if (check != checkCatagory) { check = false; checkCatagory = false; }
			}
			if (check == true && checkCatagory == true) throw new RepeatedImageException("this image exist in this gallary ");
			gallary.GallaryCatagoryId = entity.GallaryCatagoryId;

			_unitOfWork.gallaryImageRepository.Update(gallary);
			await _unitOfWork.SaveAsync();

		}
		public async Task Delete(int id)
		{
			var gallary = _unitOfWork.gallaryImageRepository.GetAll().FirstOrDefault(x => x.Id == id);
			if (gallary is null) throw new NotFoundException("There is no Gallary for delete");

			Helper.DeleteFile(@"C:\Users\Asus\Desktop\", "reactpro", "src", "assets", "images",gallary.Image);
			_unitOfWork.gallaryImageRepository.Delete(gallary);
			await _unitOfWork.SaveAsync();

		}
	}
}
