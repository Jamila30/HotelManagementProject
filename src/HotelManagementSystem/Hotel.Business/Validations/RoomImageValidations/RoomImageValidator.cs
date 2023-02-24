using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotel.Business.Validations.RoomImageValidations
{
	public class RoomImageValidator :AbstractValidator<RoomImage>
	{
		public RoomImageValidator()
		{
			RuleFor(x => x.Id)
				.NotEmpty()
				.NotNull();
			RuleFor(x => x.Image)
				.NotEmpty()
				.NotNull();
			RuleFor(x => x.FlatId)
				.NotEmpty()
				.NotNull();
		}
	}
}
