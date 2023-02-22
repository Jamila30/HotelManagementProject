using Hotel.Business.DTOs.ReservationDTOs;

namespace Hotel.Business.Validations.ReservationValidations
{
	public class CreateReservationValidator:AbstractValidator<CreateReservationDto>
	{
		public CreateReservationValidator()
		{
			RuleFor(r => r.Price).NotNull().NotEmpty();
			RuleFor(r => r.FlatId).NotNull().NotEmpty().Custom((FlatId, context) =>
			{
				if (!int.TryParse(FlatId.ToString(), out int Flat_Id))
				{
					context.AddFailure("Enter Valid Format");
				}
			});
			RuleFor(r => r.UserId).NotNull().NotEmpty().Custom((UserId, context) =>
			{
				if (!int.TryParse(UserId.ToString(), out int User_Id))
				{
					context.AddFailure("Enter Valid Format");
				}
			});
			RuleFor(r => r.Children).NotNull().NotEmpty();
			RuleFor(r => r.Adults).NotNull().NotEmpty();
			RuleFor(r => r.CheckInDate).NotNull().NotEmpty().Custom((CheckInDate, context) =>
			{
				if (CheckInDate < DateTime.Now)
				{
					context.AddFailure("Check In date can not be choosen from past");
				}
			});
			RuleFor(r => r.CheckOutDate).NotNull().NotEmpty().Custom((CheckOutDate, context) =>
			{
				if (CheckOutDate < DateTime.Now)
				{
					context.AddFailure("Check In date can not be choosen from past");
				}
			}).GreaterThan(x=>x.CheckInDate);
		}
	}
}
