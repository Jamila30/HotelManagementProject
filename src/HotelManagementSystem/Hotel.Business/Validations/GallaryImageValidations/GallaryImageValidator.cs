namespace Hotel.Business.Validations.GallaryImageValidations
{
	public class GallaryImageValidator:AbstractValidator<GallaryImageDto>
	{
		public GallaryImageValidator()
		{
			RuleFor(x => x.Id)
				.NotEmpty()
				.NotNull();
			RuleFor(x => x.Image)
				.NotEmpty()
				.NotNull();
			RuleFor(x => x.GallaryCatagoryId)
				.NotEmpty()
				.NotNull();

		}
	}
}
