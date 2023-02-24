namespace Hotel.Business.Validations.GallaryCatagoryValidations
{
	public class GallaryCatagoryValidator:AbstractValidator<GallaryCatagoryDto>
	{
		public GallaryCatagoryValidator()
		{
			RuleFor(x=>x.Id)
				.NotEmpty()
				.NotNull();
			RuleFor(x => x.Name)
				.NotEmpty()
				.NotNull()
				.MaximumLength(70);
		}
	}
}
