using Hotel.Business.DTOs.ReservationDTOs;

namespace Hotel.Business.Validations.ReservationValidations
{
	public class CreateReservationValidator:AbstractValidator<CreateReservationDto>
	{
		public CreateReservationValidator()
		{
			
			RuleFor(r => r.FlatId).NotNull().NotEmpty().Custom((FlatId, context) =>
			{
				if (!int.TryParse(FlatId.ToString(), out int Flat_Id))
				{
					context.AddFailure("Enter Valid Format");
				}
			});
			RuleFor(r => r.Children).NotNull().NotEmpty().GreaterThanOrEqualTo(0);
			RuleFor(r => r.Adults).NotNull().NotEmpty().GreaterThanOrEqualTo(1);
		
		}
	}
}
