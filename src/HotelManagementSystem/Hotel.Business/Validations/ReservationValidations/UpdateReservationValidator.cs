using Hotel.Business.DTOs.ReservationDTOs;

namespace Hotel.Business.Validations.ReservationValidations
{
	public class UpdateReservationValidator:AbstractValidator<UpdateReservationDto>
	{
		public UpdateReservationValidator()
		{
			RuleFor(r => r.Id).NotNull().NotEmpty().Custom((Id, context) =>
			{
				if (!int.TryParse(Id.ToString(), out int id))
				{
					context.AddFailure("Enter Valid Format");
				}
			});
			RuleFor(r => r.Children).NotNull().NotEmpty();
			RuleFor(r => r.Adults).NotNull().NotEmpty();
			RuleFor(r => r.StartDate).NotNull().NotEmpty().Custom((CheckInDate, context) =>
			{
				if (CheckInDate < DateTime.Now)
				{
					context.AddFailure("Check In date can not be choosen from past");
				}
			});
			RuleFor(r => r.EndDate).NotNull().NotEmpty().Custom((CheckOutDate, context) =>
			{
				if (CheckOutDate < DateTime.Now)
				{
					context.AddFailure("Check In date can not be choosen from past");
				}
			}).GreaterThan(x => x.StartDate);

			RuleFor(r => r.UserId).NotNull().NotEmpty();
			RuleFor(r => r.Price).NotNull().NotEmpty();
			RuleFor(r => r.FlatId).NotNull().NotEmpty().Custom((FlatId, context) =>
			{
				if (!int.TryParse(FlatId.ToString(), out int Flat_Id))
				{
					context.AddFailure("Enter Valid Format");
				}
			});
		}
	}
}
