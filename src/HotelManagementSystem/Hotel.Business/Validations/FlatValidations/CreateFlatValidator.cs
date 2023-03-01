namespace Hotel.Business.Validations.FlatValidations
{
	public class CreateFlatValidator:AbstractValidator<CreateFlatDto>
	{
		public CreateFlatValidator()
		{
			RuleFor(f => f.Name)
				.NotNull()
				.NotEmpty()
				.MaximumLength(70);
			RuleFor(f => f.Description)
				.NotNull()
				.NotEmpty();
			RuleFor(f => f.Price)
				.NotEmpty()
				.NotNull();
			RuleFor(f => f.Size)
				.NotEmpty()
				.NotNull();
			RuleFor(f => f.DiscountPercent)
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
