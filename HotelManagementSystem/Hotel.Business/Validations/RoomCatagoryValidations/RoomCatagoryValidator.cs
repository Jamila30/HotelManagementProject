using FluentValidation;

namespace Hotel.Business.Validations.RoomCatagoryValidations
{
	public class RoomCatagoryValidator:AbstractValidator<RoomCatagory>
	{
		public RoomCatagoryValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty()
				.NotNull()
				.MaximumLength(70);
			RuleFor(x => x.Id)
				.NotEmpty()
				.NotNull();
		}
	}
}
