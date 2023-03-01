namespace Hotel.Business.Validations.NearPlaceValidations
{
	public class CreateNearPlaceValidator:AbstractValidator<CreateNearPlaceDto>
	{
		public CreateNearPlaceValidator()
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
