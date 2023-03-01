namespace Hotel.Business.Validations.ReservationValidations
{
	public class RecomendedFlatValidator:AbstractValidator<RecomendedFlatDto>
	{
		public RecomendedFlatValidator()
		{
			RuleFor(r => r.FlatId).NotNull().NotEmpty().Custom((FlatId, context) =>
			{
				if (!int.TryParse(FlatId.ToString(), out int Flat_Id))
				{
					context.AddFailure("Enter Valid Format");
				}
			});
			RuleFor(r => r.CatagoryId).NotNull().NotEmpty().Custom((CatagoryId, context) =>
			{
				if (!int.TryParse(CatagoryId.ToString(), out int catagoryId))
				{
					context.AddFailure("Enter Valid Format");
				}
			});
			RuleFor(r => r.BedCount).NotNull().NotEmpty();
			RuleFor(r => r.Price).NotNull().NotEmpty();
		}
	}
}
