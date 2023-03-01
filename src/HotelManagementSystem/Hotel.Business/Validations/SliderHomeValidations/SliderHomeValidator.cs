namespace Hotel.Business.Validations.SliderHomeValidations
{
	public class SliderHomeValidator:AbstractValidator<SliderHomeDto>
	{
		public SliderHomeValidator()
		{
			RuleFor(x => x.Id)
				.NotNull()
				.NotEmpty();
			RuleFor(x => x.Title)
				.NotEmpty()
				.NotNull()
				.MaximumLength(70);
			RuleFor(x => x.Description)
				.NotEmpty()
				.NotNull()
				.MaximumLength(250);
			RuleFor(x=>x.Image) 
				.NotEmpty()
				.NotNull();

		}
	}
}
