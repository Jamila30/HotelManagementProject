﻿using Hotel.Business.DTOs.RoomImageDTOs;
using Hotel.Core.Entities;
using Hotel.DataAccess.Repositories.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace Hotel.Business.Services.Implementations
{
	public class RoomImageService : IRoomImageService
	{
		private readonly IRoomImageRepository _repository;
		private readonly IFlatRepository _flatRepo;
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _env;

		public RoomImageService(IRoomImageRepository repository, IMapper mapper, IFlatRepository flatRepo, IWebHostEnvironment env)
		{
			_repository = repository;
			_mapper = mapper;
			_flatRepo = flatRepo;
			_env = env;
		}

		public async Task<List<RoomImageDto>> GetAllAsync()
		{
			var list = _repository.GetAll().ToList();
			var lists = _mapper.Map<List<RoomImageDto>>(list);
			return lists;
		}

		public async Task<List<RoomImageDto>> GetByCondition(Expression<Func<RoomImage, bool>> expression)
		{

			var list = _repository.GetAll().Where(expression).ToList();
			var lists = _mapper.Map<List<RoomImageDto>>(list);
			return lists;
		}

		public async Task<RoomImageDto?> GetByIdAsync(int id)
		{
			var image = await _repository.GetByIdAsync(id);
			var roomImage = _mapper.Map<RoomImageDto>(image);
			return roomImage;
		}
		public async Task<RoomImageDto?> GetByIdFlatId(int id)
		{
			var flat = await _flatRepo.GetByIdAsync(id);
			if (flat is null) throw new NotFoundException("There is no flat with this Flat Id");
			var image = _repository.GetAll().Include(x => x.Flat).FirstOrDefault(x => x.FlatId == id);
			var roomImage = _mapper.Map<RoomImageDto>(image);
			return roomImage;
		}
		public async Task Create(CreateRoomImageDto entity)
		{
			RoomImage roomImage = new();
			var flat = await _flatRepo.GetByIdAsync(entity.FlatId);
			if (flat is null) throw new NotFoundException("There is no flat with this Flat Id");
			roomImage.FlatId = entity.FlatId;
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
				fileName = entity.Image.CopyFileTo(_env.WebRootPath, "assets", "images", "roomImage");
				roomImage.Image = fileName;

			}
			string last;
			string next;
			bool check = false;
			bool checkCatagory = false;
			var roomList = _repository.GetAll().Include(x => x.Flat).ToList();
			foreach (var item in roomList)
			{
				if (item.Image != null && roomImage.Image != null)
				{
					last = item.Image.Substring(36);
					next = roomImage.Image.Substring(36);
					if (last.Equals(next)) { check = true; }
				}
				if (item.FlatId == roomImage.FlatId)
				{
					checkCatagory = true;
				}

			}
			if (check==true && checkCatagory==true) throw new RepeatedImageException("this image exist in another catagory");
			await _repository.Create(roomImage);
			await _repository.SaveChanges();

		}

		public async Task UpdateAsync(int id, UpdateRoomImageDto entity)
		{
			if (id != entity.Id) throw new IncorrectIdException("id didnt match another id");
			var roomImage = await _repository.GetByIdAsync(id);
			if (roomImage is null) throw new NotFoundException("there is no room image for update");
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

				fileName = entity.Image.CopyFileTo(_env.WebRootPath, "assets", "images", "roomImage");
				//	roomImage.Image = fileName;
			}
			var flat = await _flatRepo.GetByIdAsync(entity.FlatId);
			if (flat is null) throw new NotFoundException("There is no flat with this Flat Id");
			string last;
			string next;
			bool checkImage = false;
			bool checkCatagory = false;
			var roomList = _repository.GetAll().Include(x => x.Flat).ToList();
			foreach (var item in roomList)
			{
				if (item.Image != null && item.Flat != null && item.Flat.RoomCatagory != null)
				{
					last = item.Image.Substring(36);
					next = fileName.Substring(36);
					if (last.Equals(next)) { checkImage = true; }
					
				}
				if (item.FlatId == entity.FlatId)
				{
					checkCatagory = true;
				}
			}
			if (checkImage==true && checkCatagory ==true) throw new RepeatedImageException("this image exist in another catagory");
			roomImage.FlatId = entity.FlatId;
			_repository.Update(roomImage);
			await _repository.SaveChanges();
		}
		public async Task Delete(int id)
		{
			var roomImage = await _repository.GetByIdAsync(id);
			if (roomImage is null) throw new NotFoundException("There is no image for delete");
			if (roomImage.Image != null)
			{
				Helper.DeleteFile(_env.WebRootPath, "assets", "images", "roomImage", roomImage.Image);
			}

			_repository.Delete(roomImage);
			await _repository.SaveChanges();
		}

	}
}
