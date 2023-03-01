namespace Hotel.Business.Services.Implementations
{
	public class FaqService : IFaqService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public FaqService(IMapper mapper, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}
		public async Task<List<FaqDto>> GetAllAsync()
		{
			var list = await _unitOfWork.fAQRepository.GetAll().ToListAsync();
			var lists = _mapper.Map<List<FaqDto>>(list);
			return lists;
		}

		public async Task<List<FaqDto>> GetByCondition(Expression<Func<FAQ, bool>> expression)
		{
			var list = await _unitOfWork.fAQRepository.GetAll().Where(expression).ToListAsync();
			var lists = _mapper.Map<List<FaqDto>>(list);
			return lists;
		}

		public async Task<FaqDto?> GetByIdAsync(int id)
		{
			var faq = await _unitOfWork.fAQRepository.GetByIdAsync(id);
			if (faq is null) throw new NotFoundException("There is no faq element for this id");
			var faqDto = _mapper.Map<FaqDto>(faq);
			return faqDto;
		}

		public async Task Create(CreateFaqDto entity)
		{
			FAQ faqElement = new FAQ()
			{
				Answer = entity.Answer,
				Question = entity.Question
			};
			await _unitOfWork.fAQRepository.Create(faqElement);
			await _unitOfWork.SaveAsync();
		}
		public async Task UpdateAsync(int id, UpdateFaqDto entity)
		{
			if (id != entity.Id) throw new IncorrectIdException("id didnt overlap");
			var faqElement = await _unitOfWork.fAQRepository.GetByIdAsync(id);
			if (faqElement is null) throw new NotFoundException("There is no faq element for this id");
			faqElement.Question = entity.Question;
			faqElement.Answer = entity.Answer;
			_unitOfWork.fAQRepository.Update(faqElement);
			await _unitOfWork.SaveAsync();
		}
		public async Task Delete(int id)
		{
			var faqElement = await _unitOfWork.fAQRepository.GetByIdAsync(id);
			if (faqElement is null) throw new NotFoundException("There is no faq element for this id");
			_unitOfWork.fAQRepository.Delete(faqElement);
			await _unitOfWork.SaveAsync();
		}
	}
}
