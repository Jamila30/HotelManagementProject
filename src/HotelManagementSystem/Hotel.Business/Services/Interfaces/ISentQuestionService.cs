namespace Hotel.Business.Services.Interfaces
{
	public interface ISentQuestionService
	{
		Task<List<SentQuestionDto>> GetAllAsync();
		Task<List<SentQuestionDto>> GetByCondition(Expression<Func<SentQuestion, bool>> expression);
		Task<SentQuestionDto?> GetByIdAsync(int id);
		Task CreateQuestion(CreateSentQuestionDto entity);
		Task AnswerQuestion(AnswerDto entity);
		Task Delete(int id);
	}
}
