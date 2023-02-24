using Hotel.DataAccess.Contexts;

namespace Hotel.Business.Services.Implementations
{
	public class GallaryImageService : IGallaryImageService
	{
		private readonly IGallaryImageRepository _repository;
		private readonly IGallaryCatagoryRepository _catRepo;
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _env;
		public GallaryImageService(IGallaryImageRepository repository, IMapper mapper, IWebHostEnvironment env, IGallaryCatagoryRepository catRepo)
		{
			_repository = repository;
			_mapper = mapper;
			_env = env;
			_catRepo = catRepo;

		}

		public async Task<List<GallaryImageDto>> GetAllAsync()
		{
			var listAll = await _repository.GetAll().ToListAsync();
			var listDto = _mapper.Map<List<GallaryImageDto>>(listAll);
			return listDto;
		}

		public async Task<List<GallaryImageDto>> GetByCondition(Expression<Func<GallaryImage, bool>> expression)
		{
			var listAll = await _repository.GetAll().Include(x => x.GallaryCatagory).Where(expression).ToListAsync();
			var listDto = _mapper.Map<List<GallaryImageDto>>(listAll);
			return listDto;
		}

		public async Task<GallaryImageDto?> GetByIdAsync(int id)
		{
			var gallary = await _repository.GetByIdAsync(id);
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

				fileName = entity.Image.CopyFileTo(_env.WebRootPath, "assets", "images", "gallaryImage");

			}
			var catagory = await _catRepo.GetByIdAsync(entity.GallaryCatagoryId);
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
			var images = _repository.GetAll().ToList();
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


			await _repository.Create(gallary);
			await _repository.SaveChanges();
		}
		public async Task UpdateAsync(int id, UpdateGallaryImageDto entity)
		{
			if (id != entity.Id) throw new IncorrectIdException("Id didnt match each other");
			var gallary = _repository.GetAll().Include(x => x.GallaryCatagory).FirstOrDefault(x => x.Id == id);
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

				fileName = entity.Image.CopyFileTo(_env.WebRootPath, "assets", "images", "gallaryImage");
				gallary.Image = fileName;
			}
			var images = _repository.GetAll().ToList();
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

			_repository.Update(gallary);
			await _repository.SaveChanges();

		}
		public async Task Delete(int id)
		{
			var gallary = _repository.GetAll().FirstOrDefault(x => x.Id == id);
			if (gallary is null) throw new NotFoundException("There is no Gallary for delete");

			Helper.DeleteFile(_env.WebRootPath, "assets", "images", "gallaryImage");
			_repository.Delete(gallary);
			await _repository.SaveChanges();

		}
	}
}
