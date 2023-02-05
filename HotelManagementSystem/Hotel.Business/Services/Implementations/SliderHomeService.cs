using AutoMapper;
using Hotel.Business.DTOs.SliderHomeDTOs;
using Hotel.Business.Exceptions;
using Hotel.Business.Services.Interfaces;
using Hotel.Business.Utilities;
using Hotel.Core.Entities;
using Hotel.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System.Linq.Expressions;

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
			var listAll = _repository.GetAll().ToList();
			var listDto = _mapper.Map<List<SliderHomeDto>>(listAll);
			return listDto;

		}

		public async Task<List<SliderHomeDto>> GetByCondition(Expression<Func<SliderHome, bool>> expression)
		{
			var listAll = _repository.GetAll().Where(expression).ToList();
			var listDto = _mapper.Map<List<SliderHomeDto>>(listAll);
			return listDto;
		}

		public async Task<SliderHomeDto?> GetByIdAsync(int id)
		{
			var slider = _repository.GetByIdAsync(id);
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
					throw new IncorrectFileSizeException("Enter Suitable File Format");
				}

				string fileName=string.Empty;
				fileName=entity.Image.CopyFileTo(_env.WebRootPath, "assets", "images");
				slider.Image = fileName;

			}
			await _repository.Create(slider);
			await _repository.SaveChanges();
		}
		public void Upate(SliderHome entity)
		{
			throw new NotImplementedException();
		}

		public void Delete(SliderHome entity)
		{
			throw new NotImplementedException();
		}



	}
}
