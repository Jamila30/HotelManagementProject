using Hotel.Business.DTOs.GallaryCatagoryDTOs;

namespace Hotel.Business.Validations.GallaryCatagoryValidations
{
	public class UpdateCatagoryValidator:AbstractValidator<UpdateCatagoryDto>
	{
		public UpdateCatagoryValidator()
		{
			RuleFor(x => x.Id).Custom((Id, context) =>
			{
				if (!int.TryParse(Id.ToString(), out int id))
				{
					context.AddFailure("Enter Valid Format");
				}
			});
			RuleFor(x => x.Name)
				.NotNull()
				.NotEmpty()
				.MaximumLength(70);
		}
	}
}
