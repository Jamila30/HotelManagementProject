namespace Hotel.Business.Validations.AmentityValidations
{
	public class CreateAmentityValidator : AbstractValidator<CreateAmentityDto>
	{
		public CreateAmentityValidator()
		{
			RuleFor(a => a.Title)
			     .NotEmpty()
			     .NotNull()
			     .MaximumLength(70);
			RuleFor(a => a.Description)
				.NotNull()
				.NotEmpty();
			RuleFor(a => a.Image)
				.NotNull()
				.NotEmpty();
		}
	}
}
