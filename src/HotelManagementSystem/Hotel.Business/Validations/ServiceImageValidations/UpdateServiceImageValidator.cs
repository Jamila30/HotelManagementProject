namespace Hotel.Business.Validations.ServiceImageValidations
{
	public class UpdateServiceImageValidator:AbstractValidator<UpdateServiceImageDto>
	{
		public UpdateServiceImageValidator()
		{
			RuleFor(x => x.Id).Custom((Id, context) =>
			{
				if (!int.TryParse(Id.ToString(), out int id))
				{
					context.AddFailure("Enter Valid Format");
				}
			});
			RuleFor(x => x.ServiceOfferId).Custom((Id, context) =>
			{
				if (!int.TryParse(Id.ToString(), out int id))
				{
					context.AddFailure("Enter Valid Format");
				}
			});
			RuleFor(x => x.ServiceOfferId)
				.NotNull()
				.NotEmpty();
		}
	}
}
