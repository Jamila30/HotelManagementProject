using Hotel.Business.DTOs.ReservationDTOs;

namespace Hotel.Business.Validations.ReservationValidations
{
	public class ReservationValidator:AbstractValidator<ReservationDto>
	{
		public ReservationValidator()
		{
			RuleFor(r=>r.Id).NotNull().NotEmpty();
			RuleFor(r=>r.FlatId).NotNull().NotEmpty();
			RuleFor(r=>r.UserId).NotNull().NotEmpty();
			RuleFor(r=>r.StartDate).NotNull().NotEmpty();
			RuleFor(r=>r.EndDate).NotNull().NotEmpty();
			RuleFor(r=>r.PhoneNumber).NotNull().NotEmpty();
			RuleFor(r=>r.GuestName).NotNull().NotEmpty();
			RuleFor(r=>r.Children).NotNull().NotEmpty();
			RuleFor(r=>r.Adults).NotNull().NotEmpty();
			RuleFor(r=>r.Price).NotNull().NotEmpty();
			RuleFor(r=>r.IsCanceled).NotNull();
			RuleFor(r=>r.IsDeleted).NotNull();

		}
	}
}
