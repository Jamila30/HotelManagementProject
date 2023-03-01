namespace Hotel.Business.Validations.NearPlaceValidations
{
	public class UpdateNearPlaceValidator:AbstractValidator<UpdateNearPlaceDto>
	{
		public UpdateNearPlaceValidator()
		{
			RuleFor(x => x.Id).Custom((Id, contex) =>
			{
				if (!int.TryParse(Id.ToString(), out int id))
				{
					contex.AddFailure("Enter true Id format");
				}
			});

			RuleFor(x => x.Title)
				.NotEmpty()
				.NotNull()
				.MaximumLength(70);
			RuleFor(x => x.Description)
				.NotEmpty()
				.NotNull()
				.MaximumLength(250);
		}
	}
}
