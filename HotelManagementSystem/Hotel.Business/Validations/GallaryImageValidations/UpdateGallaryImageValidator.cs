using Hotel.Business.DTOs.GallaryImageDTOs;

namespace Hotel.Business.Validations.GallaryImageValidations
{
	public class UpdateGallaryImageValidator:AbstractValidator<UpdateGallaryImageDto>
	{
		public UpdateGallaryImageValidator()
		{
			RuleFor(x => x.Id).Custom((Id, contex) =>
			{
				if (!int.TryParse(Id.ToString(), out int id))
				{
					contex.AddFailure("Enter true Id format");
				}
			});
			RuleFor(x => x.GallaryCatagoryId).Custom((Id, contex) =>
			{
				if (!int.TryParse(Id.ToString(), out int id))
				{
					contex.AddFailure("Enter true Id format");
				}
			});
			RuleFor(x => x.GallaryCatagoryId)
				.NotEmpty()
				.NotNull();
		}
	}
}
