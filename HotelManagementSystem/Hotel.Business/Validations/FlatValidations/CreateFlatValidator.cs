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
			RuleFor(f => f.Adults)
				.NotEmpty()
				.NotNull()
				.GreaterThanOrEqualTo(1)
				.LessThanOrEqualTo(5);
			RuleFor(f => f.Children)
				.NotEmpty()
				.NotNull()
				.GreaterThanOrEqualTo(0)
				.LessThanOrEqualTo(4);
			RuleFor(f => f.Price)
				.NotEmpty()
				.NotNull();
			RuleFor(f => f.Size)
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
