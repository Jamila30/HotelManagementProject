namespace Hotel.Business.Validations.FaqValidations
{
	public class FaqValidator:AbstractValidator<FaqDto>
	{
		public FaqValidator()
		{
			RuleFor(x => x.Id).NotEmpty().NotNull();
			RuleFor(x => x.Question).NotNull().NotEmpty();
			RuleFor(x=>x.Answer).NotNull().NotEmpty();	
		}
	}
}
