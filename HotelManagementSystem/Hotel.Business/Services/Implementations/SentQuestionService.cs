using Hotel.Business.DTOs.SentQuestionDTOs;

namespace Hotel.Business.Services.Implementations
{
	public class SentQuestionService : ISentQuestionService
	{
		private readonly ISentQuestionRepository _repostory;
		private readonly IMailService _mailService;
		private readonly IMapper _mapper;

		public SentQuestionService(ISentQuestionRepository repostory, IMapper mapper, IMailService mailService)
		{
			_repostory = repostory;
			_mapper = mapper;
			_mailService = mailService;
		}

		public async Task<List<SentQuestionDto>> GetAllAsync()
		{
			var list = await _repostory.GetAll().ToListAsync();
			var listDto = _mapper.Map<List<SentQuestionDto>>(list);
			return listDto;
		}

		public async Task<List<SentQuestionDto>> GetByCondition(Expression<Func<SentQuestion, bool>> expression)
		{
			var list = await _repostory.GetAll().Where(expression).ToListAsync();
			var listDto = _mapper.Map<List<SentQuestionDto>>(list);
			return listDto;
		}

		public async Task<SentQuestionDto?> GetByIdAsync(int id)
		{
			var result = await _repostory.GetByIdAsync(id);
			var resultDto = _mapper.Map<SentQuestionDto>(result);
			return resultDto;
		}

		public async Task CreateQuestion(CreateSentQuestionDto entity)
		{
			SentQuestion sentQuestion = new()
			{
				Question = entity.Question,
				Email = entity.Email,
				IsAnswered = false
			};
			await _repostory.Create(sentQuestion);
			await _repostory.SaveChanges();
		}
		public async Task AnswerQuestion(AnswerDto entity)
		{
			var question=await _repostory.GetAll().FirstOrDefaultAsync(x => x.Id == entity.QuestionId);
			if (question is null) throw new NotFoundException("question doesnt exist for this id");
			question.Answer=entity.Answer;
			await _mailService.SendEmailAsync(new MailRequestDto()
			{
				ToEmail = question.Email,
				Subject = "Your question aswered by Hotel ",
				Body = $"Hello dear, You send question by our website.Your question was : {question.Question}. "  +
				$" Our answer : {question.Answer}"
			}) ;
			question.IsAnswered = true;
			_repostory.Update(question);
			await _repostory.SaveChanges() ;
		}

		public async Task Delete(int id)
		{
			
			var question = await _repostory.GetAll().FirstOrDefaultAsync(x => x.Id == id);
			if (question is null) throw new NotFoundException("question doesnt exist for this id");
			_repostory.Delete(question);
			await _repostory.SaveChanges();
		}

	}
}
