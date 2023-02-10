using Hotel.Business.DTOs.FlatDTOs;

namespace Hotel.Business.Validations.FlatValidations
{
	public class FlatValidator:AbstractValidator<FlatDto>
	{
		public FlatValidator()
		{
			RuleFor(f => f.Id)
				.NotNull()
				.NotEmpty();
			RuleFor(f => f.Name)
				.NotNull()
				.NotEmpty()
				.MaximumLength(70);
			RuleFor(f => f.Description)
				.NotNull()
				.NotEmpty();
			RuleFor(f => f.Adults)
				.NotEmpty()
				.NotNull();
			RuleFor(f => f.Children)
				.NotEmpty()
				.NotNull();
			RuleFor(f => f.Price)
				.NotEmpty()
				.NotNull();
			RuleFor(f => f.Size)
				.NotEmpty()
				.NotNull();
			RuleFor(f => f.BedCount)
				.NotEmpty()
				.NotNull();
			RuleFor(f => f.RoomCatagoryId)
				.NotEmpty()
				.NotNull();

		}
	}
}
