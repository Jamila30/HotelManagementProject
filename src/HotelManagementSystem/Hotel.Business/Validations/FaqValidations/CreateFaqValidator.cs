namespace Hotel.Business.Validations.FaqValidations
{
	public class CreateFaqValidator:AbstractValidator<CreateFaqDto>
	{
		public CreateFaqValidator()
		{
			RuleFor(x => x.Question).NotNull().NotEmpty();
			RuleFor(x => x.Answer).NotNull().NotEmpty();
		}
	}
}
