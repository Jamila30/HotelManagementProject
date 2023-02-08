namespace Hotel.Business.Services.Implementations
{
	public class TeamMemberInformationService : ITeamMemberInfoService
	{
		private readonly ITeamMemberInfoRepository _repository;
		private readonly ITeamMemberRepository _teamRepo;
		private readonly IWebHostEnvironment _env;
		private readonly IMapper _mapper;
		public TeamMemberInformationService(ITeamMemberInfoRepository repository, IMapper mapper, ITeamMemberRepository teamRepo, IWebHostEnvironment env)
		{
			_repository = repository;
			_mapper = mapper;
			_teamRepo = teamRepo;
			_env = env;
		}

		public async Task<List<TeamMemberInfoDto>> GetAllAsync()
		{
			var list = _repository.GetAll().ToList();
			var listDto = _mapper.Map<List<TeamMemberInfoDto>>(list);
			return listDto;

		}

		public async Task<List<TeamMemberInfoDto>> GetByCondition(Expression<Func<TeamMemberInformation, bool>> expression)
		{
			var listAll = _repository.GetAll().Where(expression).ToList();
			var listDto = _mapper.Map<List<TeamMemberInfoDto>>(listAll);
			return listDto;
		}

		public async Task<TeamMemberInfoDto?> GetByIdAsync(int id)
		{
			var info = await _repository.GetByIdAsync(id);
			if (info is null) throw new NotFoundException("Element not found");
			var infoDto = _mapper.Map<TeamMemberInfoDto>(info);
			return infoDto;
		}

		public async Task CreateInfoForExistMember(int id, CreateTeamInfoDto entity)
		{
			if (!int.TryParse(id.ToString(), out int Id))
			{
				throw new IncorrectFormatException("Id format is wrong");
			}
			var teamMember = _teamRepo.GetAll().Include(x => x.TeamMemberInformation).FirstOrDefault(x => x.Id == id);
			if (teamMember is null) throw new NotFoundException("Didn't find any Team Member for create it's informations");

			var teamInfo = _mapper.Map<TeamMemberInformation>(entity);
			teamInfo.TeamMember = teamMember;
			await _repository.Create(teamInfo);
			await _repository.SaveChanges();

		}

		public async Task CreateInfoWithNewMember(CreateWholeInfoDto entity)
		{
			TeamMemberInformation teamInfo = new()
			{
				Facebook = entity.Facebook,
				Instagram = entity.Instagram,
				Twitter = entity.Twitter,
				Linkedin = entity.Linkedin,
				Phone = entity.Phone,
				TeamMember = new()
				{
					Fullname = entity.MemberFullname,
					Position = entity.MemberPosition,
				}
			};

			if (entity.MemberImage != null)
			{
				if (!entity.MemberImage.CheckFileSize(100))
				{
					//throw  ExceptionsDictionary.MyExceptions["Enter Suitable File Size"];
					throw new IncorrectFileSizeException("Enter Suitable File Size");
				}
				if (!entity.MemberImage.CheckFileFormat("image/"))
				{
					throw new IncorrectFileFormatException("Enter Suitable File Format");
				}

				string fileName = string.Empty;
				fileName = entity.MemberImage.CopyFileTo(_env.WebRootPath, "assets", "images", "teamMember");
				teamInfo.TeamMember.Image = fileName;

			}
			await _repository.Create(teamInfo);
			await _repository.SaveChanges();
		}
		public async Task UpdateAsync(int id, UpdateTeamMemberInfoDto entity)
		{
			if (id != entity.Id) throw new BadRequestException("Id's didn't match each other");
			//PK ve Fk eynidi deye :
			var teamMember = _teamRepo.GetAll().Include(x => x.TeamMemberInformation).FirstOrDefault(x => x.Id == id);
			if (teamMember is null) throw new NotFoundException("There is no suitable Team Member for update it's information");

			if (teamMember.TeamMemberInformation != null)
			{

				teamMember.TeamMemberInformation.Facebook = entity.Facebook;
				teamMember.TeamMemberInformation.Instagram = entity.Instagram;
				teamMember.TeamMemberInformation.Twitter = entity.Twitter;
				teamMember.TeamMemberInformation.Linkedin = entity.Linkedin;
				teamMember.TeamMemberInformation.Phone = entity.Phone;
			};

			_teamRepo.Update(teamMember);
			await _repository.SaveChanges();
		}


		public async Task Delete(int id)
		{
			var teamMember = _teamRepo.GetAll().Include(x => x.TeamMemberInformation).FirstOrDefault(x => x.Id == id);
			if (teamMember is null) throw new NotFoundException("There is no suitable Team Member for delete it's information");

			if (teamMember.TeamMemberInformation is null) throw new NotFoundException("Didnt find any info for deleting");
			_repository.Delete(teamMember.TeamMemberInformation);
			await _repository.SaveChanges();

		}


	}
}
