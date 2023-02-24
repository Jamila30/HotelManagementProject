using Hotel.Business.DTOs.ReservationDTOs;

namespace Hotel.Business.Validations.ReservationValidations
{
	public class DateValidator:AbstractValidator<DateDto>
	{
		public DateValidator()
		{
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
			}).GreaterThan(x => x.CheckInDate);

		}
	}
}
