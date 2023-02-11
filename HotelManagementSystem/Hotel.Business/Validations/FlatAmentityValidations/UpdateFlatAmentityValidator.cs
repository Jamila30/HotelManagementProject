namespace Hotel.Business.Validations.FlatAmentityValidations
{
	public class UpdateFlatAmentityValidator:AbstractValidator<UpdateFlatAmentityDto>
	{
		public UpdateFlatAmentityValidator()
		{
			RuleFor(x => x.Id)
				.Custom((Id, context) =>
				{
					if (!int.TryParse(Id.ToString(), out int id))
					{
						context.AddFailure("Enter Valid Format");
					}
				})
				.NotEmpty()
				.NotNull();
			RuleFor(x => x.FlatId)
				.NotNull()
				.NotEmpty();
			RuleFor(x => x.AmentityId)
				.NotNull()
				.NotEmpty();
		}
	}
}
