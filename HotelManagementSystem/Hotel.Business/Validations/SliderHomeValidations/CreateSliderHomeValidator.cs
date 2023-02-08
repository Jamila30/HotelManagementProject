

namespace Hotel.Business.Validations.SliderHomeValidations
{
	public class CreateSliderHomeValidator:AbstractValidator<CreateSliderHomeDto>
	{
		public CreateSliderHomeValidator()
		{
			RuleFor(x => x.Title)
				.NotEmpty()
				.NotNull()
				.MaximumLength(70);
			RuleFor(x => x.Description)
				.NotEmpty()
				.NotNull()
				.MaximumLength(250);
			RuleFor(x => x.Image)
				.NotEmpty()
				.NotNull();
		}
	}
}
