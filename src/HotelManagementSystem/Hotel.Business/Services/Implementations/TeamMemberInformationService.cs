namespace Hotel.Business.Services.Implementations
{
	public class TeamMemberInformationService : ITeamMemberInfoService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _env;
		private readonly IMapper _mapper;
		public TeamMemberInformationService( IMapper mapper, IWebHostEnvironment env, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_env = env;
			_unitOfWork = unitOfWork;
		}

		public async Task<List<TeamMemberInfoDto>> GetAllAsync()
		{
			var list = await _unitOfWork.teamMemberInfoRepository.GetAll().ToListAsync();
			var listDto = _mapper.Map<List<TeamMemberInfoDto>>(list);
			return listDto;

		}

		public async Task<List<TeamMemberInfoDto>> GetByCondition(Expression<Func<TeamMemberInformation, bool>> expression)
		{
			var listAll = await		_unitOfWork.teamMemberInfoRepository.GetAll().Where(expression).ToListAsync();
			var listDto = _mapper.Map<List<TeamMemberInfoDto>>(listAll);
			return listDto;
		}

		public async Task<TeamMemberInfoDto?> GetByIdAsync(int id)
		{
			var info = await _unitOfWork.teamMemberInfoRepository.GetByIdAsync(id);
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
			if (id != entity.TeamMemberId) throw new IncorrectIdException("id didnt overlap");
			var teamMember = await _unitOfWork.teamMemberRepository.GetByIdAsync(id);
			if (teamMember is null) throw new NotFoundException("Didn't find any Team Member for create it's informations");

			//var teamInfo=_mapper.Map<TeamMemberInformation>(entity);
			var teamInfo = new TeamMemberInformation()
			{
				Id = entity.TeamMemberId,
				Twitter = entity.Twitter,
				Facebook = entity.Facebook,
				Phone = entity.Phone,
				Instagram = entity.Instagram,
				Linkedin = entity.Linkedin,
				TeamMember = teamMember,
			};

			await _unitOfWork.teamMemberInfoRepository.Create(teamInfo);
			await	_unitOfWork.SaveAsync();

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

				try
				{
					teamInfo.TeamMember.Image = await entity.MemberImage.CopyFileToAsync(_env.WebRootPath, "assets", "images", "teamMember");

				}
				catch (Exception)
				{
					throw new BadRequestException("file didnt created");
				}

			}
			await _unitOfWork.teamMemberInfoRepository.Create(teamInfo);
			await _unitOfWork.SaveAsync();
		}
		public async Task UpdateAsync(int id, UpdateTeamMemberInfoDto entity)
		{
			if (id != entity.Id) throw new BadRequestException("Id's didn't match each other");
			//PK ve Fk eynidi deye :
			var teamMember = _unitOfWork.teamMemberRepository.GetAll().Include(x => x.TeamMemberInformation).FirstOrDefault(x => x.Id == id);
			if (teamMember is null) throw new NotFoundException("There is no suitable Team Member for update it's information");

			if (teamMember.TeamMemberInformation != null)
			{

				teamMember.TeamMemberInformation.Facebook = entity.Facebook;
				teamMember.TeamMemberInformation.Instagram = entity.Instagram;
				teamMember.TeamMemberInformation.Twitter = entity.Twitter;
				teamMember.TeamMemberInformation.Linkedin = entity.Linkedin;
				teamMember.TeamMemberInformation.Phone = entity.Phone;
			};

			_unitOfWork.teamMemberRepository.Update(teamMember);
			await _unitOfWork.SaveAsync();
		}


		public async Task Delete(int id)
		{
			var teamMember = _unitOfWork.teamMemberRepository.GetAll().Include(x => x.TeamMemberInformation).FirstOrDefault(x => x.Id == id);
			if (teamMember is null) throw new NotFoundException("There is no suitable Team Member for delete it's information");

			if (teamMember.TeamMemberInformation is null) throw new NotFoundException("Didnt find any info for deleting");
			_unitOfWork.teamMemberInfoRepository.Delete(teamMember.TeamMemberInformation);
			await _unitOfWork.SaveAsync();

		}


	}
}
