using Hotel.Business.DTOs.SelectedListDTOs;

namespace Hotel.Business.Validations.SelectedListValidations
{
	public class StablePropertyValidator:AbstractValidator<StablePropertyDto>
	{
		public StablePropertyValidator()
		{
			RuleFor(x => x.FlatId).NotNull().NotEmpty();
		}
	}
}
