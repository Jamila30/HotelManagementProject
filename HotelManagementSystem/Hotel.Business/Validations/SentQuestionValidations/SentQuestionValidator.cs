using Hotel.Business.DTOs.SentQuestionDTOs;

namespace Hotel.Business.Validations.SentQuestionValidations
{
	public class SentQuestionValidator:AbstractValidator<SentQuestionDto>
	{
		public SentQuestionValidator()
		{
			RuleFor(x=>x.Email).NotNull().NotEmpty().EmailAddress();
			RuleFor(x=>x.Id).NotNull().NotEmpty().Custom((Id, context) =>
			{
				if (!int.TryParse(Id.ToString(), out int id))
				{
					context.AddFailure("Enter True Format");
				}
			});
			RuleFor(x=>x.Question).NotNull().NotEmpty();
			RuleFor(x => x.IsAnswered).NotNull();
		}
	}
}
