namespace Hotel.Business.Validations.AmentityValidations
{
	public class AmentityValidator:AbstractValidator<AmentityDto>
	{
		public AmentityValidator()
		{
			RuleFor(a=>a.Id)
				.NotNull()
				.NotEmpty();
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
