namespace Hotel.Business.Validations.AmentityValidations
{
	public class UpdateAmentityValidator : AbstractValidator<UpdateAmentityDto>
	{
		public UpdateAmentityValidator()
		{
			RuleFor(c => c.Id)
				.Custom((Id, context) =>
				{
					if (!int.TryParse(Id.ToString(), out int id))
					{
						context.AddFailure("Enter Valid Format");
					}
				})
				 .NotNull()
				 .NotEmpty();
			RuleFor(a => a.Title)
				 .NotEmpty()
				 .NotNull()
				 .MaximumLength(70);
			RuleFor(a => a.Description)
				.NotNull()
				.NotEmpty();
		}
	}
}
