namespace Hotel.Business.Services.Implementations
{
	public class TeamMemberService : ITeamMemberService
	{
		private readonly ITeamMemberRepository _repository;
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _env;

		public TeamMemberService(ITeamMemberRepository repository, IMapper mapper, IWebHostEnvironment env)
		{
			_repository = repository;
			_mapper = mapper;
			_env = env;
		}

		public async Task<List<TeamMemberDto>> GetAllAsync()
		{
			var list = await _repository.GetAll().ToListAsync();
			var listDto = _mapper.Map<List<TeamMemberDto>>(list);
			return listDto;
		}

		public async Task<List<TeamMemberDto>> GetByCondition(Expression<Func<TeamMember, bool>> expression)
		{
			var listAll = await _repository.GetAll().Where(expression).ToListAsync();
			var listDto = _mapper.Map<List<TeamMemberDto>>(listAll);
			return listDto;
		}

		public async Task<OneMemberInfoDto?> GetByIdAsync(int id)
		{
			var info = await _repository.GetAll().Include(x => x.TeamMemberInformation).FirstOrDefaultAsync(x => x.Id == id);
			if (info is null) throw new NotFoundException("Element not found");
			OneMemberInfoDto oneMember = new()
			{
				Fullname = info.Fullname,
				Position = info.Position,
				Image = info.Image
			};
			if (info.TeamMemberInformation != null)
			{
				oneMember.Twitter = info.TeamMemberInformation.Twitter;
				oneMember.Instagram = info.TeamMemberInformation.Instagram;
				oneMember.Facebook = info.TeamMemberInformation.Facebook;
				oneMember.Linkedin = info.TeamMemberInformation.Linkedin;
				oneMember.Phone = info.TeamMemberInformation.Phone;
			}
			//If it is null we said it is possible in Validation
			return oneMember;

		}

		public async Task CreateTeamWithInfo(CreateWholeMemberDto createWholeMember)
		{
			TeamMember teamMember = new()
			{
				Fullname = createWholeMember.Fullname,
				Position = createWholeMember.Position,
				TeamMemberInformation = new()
				{
					Facebook = createWholeMember.Facebook,
					Linkedin = createWholeMember.Linkedin,
					Phone = createWholeMember.Phone,
					Instagram = createWholeMember.Instagram,
					Twitter = createWholeMember.Twitter
				}
			};
			if (createWholeMember.Image != null)
			{
				if (!createWholeMember.Image.CheckFileSize(100))
				{
					//throw  ExceptionsDictionary.MyExceptions["Enter Suitable File Size"];
					throw new IncorrectFileSizeException("Enter Suitable File Size");
				}
				if (!createWholeMember.Image.CheckFileFormat("image/"))
				{
					throw new IncorrectFileFormatException("Enter Suitable File Format");
				}

				
				teamMember.Image = createWholeMember.Image.CopyFileTo(_env.WebRootPath, "assets", "images", "teamMember");

			}

			await _repository.Create(teamMember);
			await _repository.SaveChanges();
		}
		public async Task Create(CreateTeamMemberDto createTeam)
		{

			TeamMember teamMember = new()
			{
				Fullname = createTeam.Fullname,
				Position = createTeam.Position,

			};
			if (createTeam.Image != null)
			{
				if (!createTeam.Image.CheckFileSize(100))
				{
					//throw  ExceptionsDictionary.MyExceptions["Enter Suitable File Size"];
					throw new IncorrectFileSizeException("Enter Suitable File Size");
				}
				if (!createTeam.Image.CheckFileFormat("image/"))
				{
					throw new IncorrectFileFormatException("Enter Suitable File Format");
				}

				teamMember.Image = createTeam.Image.CopyFileTo(_env.WebRootPath, "assets", "images", "teamMember");
				

			}

			await _repository.Create(teamMember);
			await _repository.SaveChanges();

		}

		public async Task UpdateAsync(int id, UpdateTeamMemberDto entity)
		{
			if (id != entity.Id) throw new BadRequestException("Id's didn't match each other");
			var teamMember = _repository.GetAll().Include(x => x.TeamMemberInformation).FirstOrDefault(x => x.Id == id);
			if (teamMember is null) throw new NotFoundException("There is no suitable Team Member for update");
			teamMember.Position = entity.Position;
			teamMember.Fullname = entity.Fullname;
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
				fileName = entity.Image.CopyFileTo(_env.WebRootPath, "assets", "images", "teamMember");
				teamMember.Image = fileName;

			}
			_repository.Update(teamMember);
			await _repository.SaveChanges();
		}


		public async Task Delete(int id)
		{
			var teamMember = _repository.GetAll().Include(x => x.TeamMemberInformation).FirstOrDefault(x => x.Id == id);
			if (teamMember is null) throw new NotFoundException("There is no suitable Team Member for delete");
			_repository.Delete(teamMember);
			await _repository.SaveChanges();
		}

	}
}
