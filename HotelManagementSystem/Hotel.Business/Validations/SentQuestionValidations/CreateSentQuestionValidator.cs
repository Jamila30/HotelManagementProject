using Hotel.Business.DTOs.SentQuestionDTOs;

namespace Hotel.Business.Validations.SentQuestionValidations
{
	public class CreateSentQuestionValidator:AbstractValidator<CreateSentQuestionDto>
	{
		public CreateSentQuestionValidator()
		{
			RuleFor(x=>x.Email).NotEmpty().NotNull().EmailAddress().MaximumLength(120);
			RuleFor(x=>x.Question).NotEmpty().NotNull();
		}
	}
}
