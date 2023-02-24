using Hotel.Business.DTOs.FaqDTOs;
using Hotel.Core.Entities;

namespace Hotel.Business.Services.Implementations
{
	public class FaqService : IFaqService
	{
		private readonly IFAQRepository _repository;
		private readonly IMapper _mapper;

		public FaqService(IFAQRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}
		public async Task<List<FaqDto>> GetAllAsync()
		{
			var list = await _repository.GetAll().ToListAsync();
			var lists = _mapper.Map<List<FaqDto>>(list);
			return lists;
		}

		public async Task<List<FaqDto>> GetByCondition(Expression<Func<FAQ, bool>> expression)
		{
			var list = await _repository.GetAll().Where(expression).ToListAsync();
			var lists = _mapper.Map<List<FaqDto>>(list);
			return lists;
		}

		public async Task<FaqDto?> GetByIdAsync(int id)
		{
			var faq = await _repository.GetByIdAsync(id);
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
			await _repository.Create(faqElement);
			await _repository.SaveChanges();
		}
		public async Task UpdateAsync(int id, UpdateFaqDto entity)
		{
			if (id != entity.Id) throw new IncorrectIdException("id didnt overlap");
			var faqElement = await _repository.GetByIdAsync(id);
			if (faqElement is null) throw new NotFoundException("There is no faq element for this id");
			faqElement.Question = entity.Question;
			faqElement.Answer = entity.Answer;
			_repository.Update(faqElement);
			await _repository.SaveChanges();
		}
		public async Task Delete(int id)
		{
			var faqElement = await _repository.GetByIdAsync(id);
			if (faqElement is null) throw new NotFoundException("There is no faq element for this id");
			_repository.Delete(faqElement);
			await _repository.SaveChanges();
		}
	}
}
