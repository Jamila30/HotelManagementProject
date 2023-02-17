namespace Hotel.Business.Services.Implementations
{
	public class CommentService : ICommentService
	{
		private readonly ICommentRepository _repository;
		private readonly IMapper _mapper;
		private readonly IFlatRepository _flatRepo;
		private readonly UserManager<AppUser> _userManager;
		public CommentService(ICommentRepository repository, IMapper mapper, IFlatRepository flatRepo, UserManager<AppUser> userManager)
		{
			_repository = repository;
			_mapper = mapper;
			_flatRepo = flatRepo;
			_userManager = userManager;
		}

		public async Task<List<CommentDto>> GetAllAsync()
		{
			var list = await _repository.GetAll().ToListAsync();
			var listDto = _mapper.Map<List<CommentDto>>(list);
			return listDto;
		}

		public async Task<List<CommentDto>> GetByCondition(Expression<Func<Comment, bool>> expression)
		{
			var list = await _repository.GetAll().Where(expression).ToListAsync();
			var listDto = _mapper.Map<List<CommentDto>>(list);
			return listDto;
		}

		public async Task<CommentDto?> GetByIdAsync(int id)
		{
			var comment = await _repository.GetByIdAsync(id);
			var commentDto = _mapper.Map<CommentDto>(comment);
			return commentDto;
		}

		public async Task Create(CreateCommentDto entity)
		{
			var flat = await _flatRepo.GetByIdAsync(entity.FlatId);
			if (flat is null) throw new NotFoundException("There is no flat with this id for create");
			var user = await _userManager.FindByIdAsync(entity.UserId);
			if (user is null) throw new NotFoundException("There is no user with this id for create");

			var comment = _mapper.Map<Comment>(entity);
			comment.Created = DateTime.UtcNow;
			comment.Flat = flat;
			comment.User= user;
			if (flat.Comments != null&& user.Comments!=null)
			{
				flat.Comments.Add(comment);
				user.Comments.Add(comment);
			}
			await _repository.Create(comment);
			await _repository.SaveChanges();
		}
		public async Task UpdateAsync(int id, UpdateCommentDto entity)
		{
			if (id != entity.Id) throw new IncorrectIdException("id didt match another");
			var flat = await _flatRepo.GetByIdAsync(entity.FlatId);
			if (flat is null) throw new NotFoundException("There is no flat with this Flat Id");
			var user = await _userManager.FindByIdAsync(entity.UserId);
			if (user is null) throw new NotFoundException("There is no user with this id for create");
			var comment = await _repository.GetByIdAsync(id);
			if (comment is null) throw new NotFoundException("there is no comment  with this id");
			comment.Id = entity.Id;
			comment.Name = entity.Name;
			comment.Opinions = entity.Opinions;
			comment.UserId = entity.UserId;
			comment.FlatId = entity.FlatId;
			_repository.Update(comment);
			await _repository.SaveChanges();
		}
		public async Task Delete(int id)
		{

			var comment = _repository.GetAll().FirstOrDefault(x => x.Id == id);
			if (comment is null) throw new NotFoundException("there is no comment to delete");
			_repository.Delete(comment);
			await _repository.SaveChanges();
		}


	}
}
