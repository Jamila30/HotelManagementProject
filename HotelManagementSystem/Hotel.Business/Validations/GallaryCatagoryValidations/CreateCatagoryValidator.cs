namespace Hotel.Business.Validations.GallaryCatagoryValidations
{
	public class CreateCatagoryValidator:AbstractValidator<CreateCatagoryDto>
	{
		public CreateCatagoryValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty()
				.NotNull()
				.MaximumLength(70);
		}
	}
}
