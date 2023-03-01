namespace Hotel.Business.Services.Implementations
{
	public class SentQuestionService : ISentQuestionService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMailService _mailService;
		private readonly IMapper _mapper;

		public SentQuestionService(IMapper mapper, IMailService mailService, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_mailService = mailService;
			_unitOfWork = unitOfWork;
		}

		public async Task<List<SentQuestionDto>> GetAllAsync()
		{
			var list = await _unitOfWork.sentQuestionRepository.GetAll().ToListAsync();
			var listDto = _mapper.Map<List<SentQuestionDto>>(list);
			return listDto;
		}

		public async Task<List<SentQuestionDto>> GetByCondition(Expression<Func<SentQuestion, bool>> expression)
		{
			var list = await _unitOfWork.sentQuestionRepository.GetAll().Where(expression).ToListAsync();
			var listDto = _mapper.Map<List<SentQuestionDto>>(list);
			return listDto;
		}

		public async Task<SentQuestionDto?> GetByIdAsync(int id)
		{
			var result = await _unitOfWork.sentQuestionRepository.GetByIdAsync(id);
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

			await _unitOfWork.sentQuestionRepository.Create(sentQuestion);
			await _unitOfWork.SaveAsync();
		}
		public async Task AnswerQuestion(AnswerDto entity)
		{
			var question = await _unitOfWork.sentQuestionRepository.GetAll().FirstOrDefaultAsync(x => x.Id == entity.QuestionId);
			if (question is null) throw new NotFoundException("question doesnt exist for this id");
			question.Answer = entity.Answer;
			await _mailService.SendEmailAsync(new MailRequestDto()
			{
				ToEmail = question.Email,
				Subject = "Your question aswered by Hotel ",
				Body = $"Hello dear, You send question by our website.Your question was : {question.Question}. " +
				$" Our answer : {question.Answer}"
			});
			question.IsAnswered = true;
			_unitOfWork.sentQuestionRepository.Update(question);
			await _unitOfWork.SaveAsync();
		}

		public async Task Delete(int id)
		{
			var question = await _unitOfWork.sentQuestionRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
			if (question is null) throw new NotFoundException("question doesnt exist for this id");
			_unitOfWork.sentQuestionRepository.Delete(question);
			await _unitOfWork.SaveAsync();
		}

	}
}
