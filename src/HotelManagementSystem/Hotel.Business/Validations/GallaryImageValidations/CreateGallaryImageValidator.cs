namespace Hotel.Business.Validations.GallaryImageValidations
{
	public class CreateGallaryImageValidator:AbstractValidator<CreateGallaryImageDto>
	{
		public CreateGallaryImageValidator()
		{
			RuleFor(x => x.Image)
				.NotEmpty()
				.NotNull();
			RuleFor(x => x.GallaryCatagoryId)
				.NotEmpty()
				.NotNull();
		}
	}
}
