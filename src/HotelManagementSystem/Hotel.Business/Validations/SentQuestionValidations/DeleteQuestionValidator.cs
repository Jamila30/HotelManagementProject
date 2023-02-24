using Hotel.Business.DTOs.SentQuestionDTOs;

namespace Hotel.Business.Validations.SentQuestionValidations
{
	public class DeleteQuestionValidator:AbstractValidator<DeleteQuestionDto>
	{
		public DeleteQuestionValidator()
		{
			RuleFor(x => x.QuestionId).NotNull().NotEmpty().Custom((Id, context) =>
			{
				if (!int.TryParse(Id.ToString(), out int id))
				{
					context.AddFailure("Enter True Format");
				}
			}); ;
		}
	}
}
