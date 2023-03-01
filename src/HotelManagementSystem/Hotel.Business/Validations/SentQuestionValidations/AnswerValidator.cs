namespace Hotel.Business.Validations.SentQuestionValidations
{
	public class AnswerValidator:AbstractValidator<AnswerDto>
	{
		public AnswerValidator()
		{
			RuleFor(x=>x.Answer).NotNull().NotEmpty();
			RuleFor(x => x.QuestionId).NotNull().NotEmpty().Custom((Id, context) =>
			{
				if(!int.TryParse(Id.ToString(),out int id))
				{
					context.AddFailure("Enter True Format");
				}
			});

		}
	}
}
