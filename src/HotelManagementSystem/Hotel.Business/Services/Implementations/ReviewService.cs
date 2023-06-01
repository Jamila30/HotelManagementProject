using entities = Hotel.Core.Entities;
namespace Hotel.Business.Services.Implementations
{
	public class ReviewService : IReviewService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<AppUser> _userManager;
		private readonly IMapper _mapper;
		public ReviewService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_userManager = userManager;
		}

		public async Task<List<ReviewDto>> GetAllAsync()
		{
			var list = await _unitOfWork.reviewRepository.GetAll().ToListAsync();
			var lists = _mapper.Map<List<ReviewDto>>(list);
			return lists;
		}

		public async Task<List<ReviewDto>> GetByCondition(Expression<Func<Core.Entities.Review, bool>> expression)
		{
			var list = await _unitOfWork.reviewRepository.GetAll().Where(expression).ToListAsync();
			var lists = _mapper.Map<List<ReviewDto>>(list);
			return lists;
		}

		public async Task<ReviewDto?> GetByIdAsync(int id)
		{
			var review = await _unitOfWork.reviewRepository.GetByIdAsync(id);
			var reviewDto = _mapper.Map<ReviewDto>(review);
			return reviewDto;
		}
		public async Task Create(CreateReviewDto entity)
		{
			var flat = await _unitOfWork.flatRepository.GetAll().Include(f=>f.Reviews).FirstOrDefaultAsync(f=>f.Id==entity.FlatId);
			if (flat is null) throw new NotFoundException("there is not flat with this id");
			var user = await _userManager.FindByIdAsync(entity.UserId);
			if (user is null) throw new NotFoundException("there is not user with this id");
			if(_unitOfWork.reviewRepository.GetAll().Any(r=>r.FlatId==entity.FlatId && r.UserId.Equals(entity.UserId)))
			{
				throw new AlreadyExistException("this user added any review for this flat");
			}
			entities.Review review = new entities.Review()
			{
				FlatId = entity.FlatId,
				UserId = entity.UserId,
				Opinions = entity.Opinions,
				Rate = entity.Rate,
				Flat = flat,
				User = user
			};
			flat.Reviews.Add(review);
			user.Reviews.Add(review);
			var wholeRating = 0;
			var count = flat.Reviews.Count;
			foreach (var item in flat.Reviews)
			{
				wholeRating = wholeRating+item.Rate;
			}
			flat.Rating = wholeRating / count;
			await _unitOfWork.reviewRepository.Create(review);
			await _unitOfWork.SaveAsync();
		}
		public async Task Update(int id, UpdateReviewDto entity)
		{
			if (id != entity.Id) throw new IncorrectIdException("Id must be same");
			var review= await _unitOfWork.reviewRepository.GetByIdAsync(id);
			if (review is null) throw new NotFoundException("there is not review with this id");
            review.Rate=entity.Rate;
			review.Opinions=entity.Opinions;
			var flat = await _unitOfWork.flatRepository.GetAll().Include(f => f.Reviews).FirstOrDefaultAsync(f => f.Id == review.FlatId);
			var wholeRating = 0;
			var count = flat.Reviews.Count;
			foreach (var item in flat.Reviews)
			{
				wholeRating = wholeRating + item.Rate;
			}
			flat.Rating = wholeRating / count;
			_unitOfWork.reviewRepository.Update(review);
			 await _unitOfWork.SaveAsync();
		}
		public async Task Delete(int id)
		{
			var review= await _unitOfWork.reviewRepository.GetByIdAsync(id);
			if (review is null) throw new NotFoundException("there is not review with this id");
			_unitOfWork.reviewRepository.Delete(review);
			await _unitOfWork.SaveAsync();
		}

	}
}
